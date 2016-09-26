using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Devi.ParkingService.Models
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get;set; }

        public string Name { get;set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>(); 
    } 
}
