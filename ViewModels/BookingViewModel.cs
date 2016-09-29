using System.Collections.Generic;
using System.Linq;
using Devi.ParkingService.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Devi.ParkingService.ViewModels
{
    public class BookingViewModel
    {
        public string CarNumber { get; set; }

        public int SelectedLocation { get; set; }

        public int SelectedArea { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get;set; }

        public List<Location> Locations { get; set; } = new List<Location>();

        public List<Area> Areas { get; set; } = new List<Area>();

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

        public IEnumerable<SelectListItem> AvailableAreas
        {
            get
            {
                return Areas.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
            }
        }
    }
}
