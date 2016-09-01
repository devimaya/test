using System;

namespace Devi.ParkingService.Models
{
    public class Booking
    {
        public int Id { get;set; }
        public int CustomerId { get;set; }
        public int CarId { get;set; }
        public int AreaId { get;set; }
        public int LocationId { get;set; }
        public DateTime start { get;set; }
        public DateTime end { get;set; }
        
    } 
}
