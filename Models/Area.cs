using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Devi.ParkingService.Models
{
    public class Area
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Location")]
        public int LocationId { get; set; }

        public Location Location { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    } 
}
