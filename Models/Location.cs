using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Devi.ParkingService.Models
{
    public class Location
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Area> Areas { get; set; } = new List<Area>();
    }
}
