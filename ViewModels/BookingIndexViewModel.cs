using System.Collections.Generic;
using System.Linq;
using Devi.ParkingService.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Devi.ParkingService.ViewModels
{
    public class BookingIndexViewModel
    {
        public Location SelectedLocation { get; set; }

        public List<Location> Locations { get; set; } = new List<Location>();

        public IEnumerable<SelectListItem> AvailableLocations
        {
            get
            {
                return Locations.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
            }
        }
    }
}
