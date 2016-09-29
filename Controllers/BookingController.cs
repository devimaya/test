using System;
using System.Collections.Generic;
using System.Linq;
using Devi.ParkingService.DataAccess;
using Devi.ParkingService.Models;
using Devi.ParkingService.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var vm = new BookingViewModel();
            vm.Locations = _context.Locations.ToList();
            return View(vm);
        }

        [HttpPost]
        public IActionResult FindAreas(BookingViewModel vm)
        {
            var availableAreas = FindAvailableAreas(vm);
            vm.Areas = availableAreas;
            vm.Locations = _context.Locations.ToList();
            return View("ChooseArea", vm);
        }

        private List<Area> FindAvailableAreas(BookingViewModel vm)
        {
            var startTime = DateTime.Parse(vm.StartTime);
            var endTime = DateTime.Parse(vm.EndTime);
            var location = _context.Locations
                .Include(x => x.Areas).ThenInclude(x => x.Bookings)
                .First(x => x.Id == vm.SelectedLocation);
            var results = new List<Area>();
            foreach (var area in location.Areas)
            {
                if (area.Bookings.Any(x => startTime >= x.StartTime && endTime <= x.EndTime)) {
                    continue;
                }

                results.Add(area);
            }

            return results;
        }

        [HttpPost]
        public IActionResult SaveBooking(BookingViewModel vm)
        {
            var booking = new Booking
            {
                AreaId = vm.SelectedArea,
                StartTime = DateTime.Parse(vm.StartTime),
                EndTime = DateTime.Parse(vm.EndTime),
                CustomerId = _context.Customers.First().Id
            };
            _context.Bookings.Add(booking);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Your booking has been successful";
            return RedirectToAction("ShowBooking", new { id = booking.Id });
        }

        [HttpGet]
        public IActionResult ShowBooking(int id)
        {
            var booking = _context.Bookings
                .Include(x => x.Customer)
                .Include(x => x.Area).ThenInclude(x => x.Location)
                .First(x => x.Id == id);
            var vm = new BookingDetailsViewModel
            {
                CustomerName = booking.Customer.Name,
                CarNumber = booking.CarNumber,
                Location = booking.Area.Location.Name,
                Area = booking.Area.Name,
                StartTime = booking.StartTime.ToString("dd/MM/yyyy HH:mm"),
                EndTime = booking.EndTime.ToString("dd/MM/yyyy HH:mm") 
            };

            return View("BookingDetails", vm);
        }
    }
}
