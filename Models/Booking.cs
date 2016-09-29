using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Devi.ParkingService.Models
{
    public class Booking
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public string CarNumber { get; set; }

        [ForeignKey("Area")]
        public int AreaId { get; set; }

        public Area Area { get; set; }

        public DateTime StartTime { get; set; }
        
        public DateTime EndTime { get; set; }
        
    } 
}
