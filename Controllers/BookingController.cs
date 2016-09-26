using System;
using System.Collections.Generic;
using System.Linq;
using Devi.ParkingService.DataAccess;
using Devi.ParkingService.Models;
using Devi.ParkingService.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Devi.ParkingService.Controllers
{
    public class BookingController : Controller
    {
        private ParkingDbContext _context;

        public BookingController(ParkingDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var vm = new BookingIndexViewModel();
            vm.Locations = _context.Locations.ToList();
            return View(vm);
        }
    }
}
