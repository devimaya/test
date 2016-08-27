using Devi.ParkingService.Models;
using Microsoft.AspNetCore.Mvc;

namespace Devi.ParkingService.Controllers
{
    public class ParkingController : Controller
    {
        [HttpGet]
        public JsonResult GetCustomer()
        {
            return Json(new
            {
                name = "totoro"
            });
        }

        [HttpPost]
        public JsonResult SaveCustomer(Customer customer)
        {
            // you can save customer to database here

            return Json(customer);
        }
    }
}
