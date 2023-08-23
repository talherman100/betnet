using System;

namespace CarRental.Models
{
    public class CarRent
    {
        public Car Car { get; set; }
        public DateRange DateRange { get; set; }
        public int UserId { get; set; }
        public int RentalId { get; set; }



    }
    //https://stackoverflow.com/questions/51916951/filter-or-check-date-in-a-date-range
    public class DateRange
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
