using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRental.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Discounts { get; set; }
        public int MinimumDriverAge { get; set; }
        public string Location { get; set; }
        public string AvailableExtras { get; set; }
        public CarGroup CarGroup { get; set; }

    }
}
