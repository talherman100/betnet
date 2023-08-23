using CarRental.Enums;
using System;

namespace CarRental.Models
{
    public class Search
    {
        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }
        public string Location { get; set; }
        public AgeGroupsEnum AgeGroup { get; set; }
        public int CarGroupId { get; set; }


    }
}
