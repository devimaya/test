using System.Collections.Generic;
using Devi.ParkingService.Models;
using Microsoft.AspNetCore.Mvc;

namespace Devi.ParkingService.Controllers
{

    public class ParkingController : Controller
    {
        private static List<Area> areaList = new List<Area>();

        public ParkingController()
        {
            areaList.Add(new Area{
                Name="P1",
                Id=1,
            });
            areaList.Add(new Area{
                Name="P2",
                Id=2,
            });
        }

        [HttpGet]
        public JsonResult GetCustomer()
        {
            return Json(new
            {
                name = "totoro"
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

        [HttpGet]
        public JsonResult SaveArea(string name, int id)
        {
            Area newArea = new Area
            {
                Name=name,
                Id=id
            };
            areaList.Add(newArea);
            return Json(newArea);

        }
    }
}
