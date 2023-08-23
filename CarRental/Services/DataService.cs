using CarRental.Enums;
using CarRental.Interfaces;
using CarRental.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CarRental.Services
{
    public class DataService : IDataService
    {
        private List<Car> _cars;
        private List<CarRent> _carRentals;
        private List<CarGroup> _carGroups;

        public DataService() {
            _carGroups = new List<CarGroup>() {
                new CarGroup() {Name = "group1", CarGroupId =1 },
                new CarGroup() {Name = "group2", CarGroupId =2 },
                new CarGroup() {Name = "group3", CarGroupId =3 },
                new CarGroup() {Name = "group4", CarGroupId =4 },
                new CarGroup() {Name = "group5", CarGroupId =5 }
            };
            _cars = new List<Car>() { 
                new Car() {CarGroup = _carGroups[0], CarId = 1, AvailableExtras = "GPS, disk", Description = "This is car with carId 1", Discounts = 10, Location = "TelAviv", MinimumDriverAge = 17, Price = 220 },
                new Car() {CarGroup = _carGroups[0], CarId = 2, AvailableExtras = "GPS", Description = "This is car with carId 2", Discounts = 0, Location = "Heifa", MinimumDriverAge = 19, Price = 444 },
                new Car() {CarGroup = _carGroups[0], CarId = 3, AvailableExtras = "GPS", Description = "This is car with carId 3", Discounts = 2, Location = "TelAviv", MinimumDriverAge = 30, Price = 111 },
                new Car() {CarGroup = _carGroups[1], CarId = 4, AvailableExtras = "No Extras", Description = "This is car with carId 4", Discounts = 0, Location = "Heifa", MinimumDriverAge = 35, Price = 220 },
                new Car() {CarGroup = _carGroups[1], CarId = 5, AvailableExtras = "GPS, disk", Description = "This is car with carId 5", Discounts = 10, Location = "TelAviv", MinimumDriverAge = 70, Price = 666 },
                new Car() {CarGroup = _carGroups[2], CarId = 6, AvailableExtras = "GPS, disk", Description = "This is car with carId 6", Discounts = 20, Location = "Heifa", MinimumDriverAge = 5, Price = 220 },
                new Car() {CarGroup = _carGroups[2], CarId = 7, AvailableExtras = "GPS, disk", Description = "This is car with carId 7", Discounts = 40, Location = "TelAviv", MinimumDriverAge = 24, Price = 888 }
            };
            _carRentals = new List<CarRent>() { 
                new CarRent() { RentalId= 1, Car = _cars[0], DateRange = new DateRange(){ StartDate = new System.DateTime(2023,10,1), EndDate = new System.DateTime(2023,11,1)}, UserId = 1 },
                new CarRent() { RentalId= 2,Car = _cars[0], DateRange = new DateRange(){ StartDate = new System.DateTime(2023,11,10), EndDate = new System.DateTime(2023,12,10)}, UserId = 2 },
                new CarRent() { RentalId= 3,Car = _cars[0], DateRange = new DateRange(){ StartDate = new System.DateTime(2023,12,20), EndDate = new System.DateTime(2023,12,30)}, UserId = 3 },
                new CarRent() { RentalId= 4,Car = _cars[1], DateRange = new DateRange(){ StartDate = new System.DateTime(2023,10,1), EndDate = new System.DateTime(2023,11,1)}, UserId = 4 },
                new CarRent() { RentalId= 5,Car = _cars[1], DateRange = new DateRange(){ StartDate = new System.DateTime(2023,11,10), EndDate = new System.DateTime(2023,12,10)}, UserId = 5 },
                new CarRent() { RentalId= 6,Car = _cars[1], DateRange = new DateRange(){ StartDate = new System.DateTime(2023,12,20), EndDate = new System.DateTime(2023,12,30)}, UserId = 6 },
                new CarRent() { RentalId= 7,Car = _cars[2], DateRange = new DateRange(){ StartDate = new System.DateTime(2023,10,1), EndDate = new System.DateTime(2023,11,1)}, UserId = 7 },
                new CarRent() { RentalId= 8,Car = _cars[2], DateRange = new DateRange(){ StartDate = new System.DateTime(2023,11,10), EndDate = new System.DateTime(2023,12,10)}, UserId = 8 },
                new CarRent() { RentalId= 9,Car = _cars[2], DateRange = new DateRange(){ StartDate = new System.DateTime(2023,12,20), EndDate = new System.DateTime(2023,12,30)}, UserId = 9 }
            };

        }
        public List<Car> GetFilteredCars(Search search)
        {
            //Available rentals are searchable by a
            //combination of dates
            //, locations,------------
            //drivers’ age groups,----------
            //and car group

            List<Car> noDatesCars;
            if (search.StartDate != null && search.EndDate != null)
            {
                //get all cars that have rentals with matched dates
                noDatesCars = (from carRental in _carRentals
                               where DateRangesOverlap(carRental.DateRange, new DateRange() { StartDate = search.StartDate, EndDate = search.EndDate })
                               select carRental.Car).ToList();
            }
            else {
                noDatesCars = new List<Car>();
            }

            //filter the cars with the matched dates
            List<Car> datesCars = _cars.FindAll(carA => !noDatesCars.Any(carB => carA.CarId == carB.CarId));

            //filter the cars by: locations, drivers’ age groups, car groups
            List<Car> filteredCars = (from car in datesCars
                                             where (search.Location.Length == 0 || search.Location.Equals(car.Location)) &&
                                                   AgeGroupMatchMin(search.AgeGroup, car.MinimumDriverAge) &&
                                                   (search.CarGroupId == 0 || car.CarGroup.CarGroupId == search.CarGroupId)
                                             select car).ToList();
            return filteredCars.ToList();
        }
        private bool AgeGroupMatchMin(AgeGroupsEnum ageGroupsEnum, int minDriverAge)
        {
            if (ageGroupsEnum == AgeGroupsEnum.All) {
                return true;
            }
            else if (ageGroupsEnum == AgeGroupsEnum.ZeroToEighteen)
            {
                return minDriverAge <= 18;
            }
            else if (ageGroupsEnum == AgeGroupsEnum.NineteenToThirty) {
                return minDriverAge <= 30;
            }
            else if (ageGroupsEnum == AgeGroupsEnum.ThirtyoneToEnd)
            {
                return minDriverAge <= int.MaxValue;
            }
            return false;
        }
        private bool DateRangesOverlap(DateRange range1, DateRange range2)
        {          //range2 starts before range1 and ends after range1
            return (range1.StartDate >= range2.StartDate && range1.StartDate <= range2.EndDate) ||
                   //range1 starts before range2 and ends after range2
                   (range1.EndDate >= range2.StartDate && range1.EndDate <= range2.EndDate) ||
                   //range2 is a small part of range1
                   (range1.StartDate <= range2.StartDate && range1.EndDate >= range2.EndDate) ||
                   //range1 is a small part of range2
                   (range1.StartDate >= range2.StartDate && range1.EndDate <= range2.EndDate);
        }
        public void Add(Car car)
        {
            if (car == null)
            {
                //write to log : "Create failed"
                throw new System.Exception("Create failed");
            }

            if (_cars.Count == 0)
            {
                //No cars-- set the added carId to 1
                car.CarId = 1;
            }
            else
            {
                //Set the added car's carId to the max carId + 1
                car.CarId = _cars.Max(x => x.CarId) + 1;
            }
            _cars.Add(car);
        }

        public void Delete(int carIdToDelete)
        {
            int carIndex = _cars.FindIndex(car => car.CarId == carIdToDelete);
            if (carIndex < 0)
            {
                //write to log : "Delete failed : Car with id: " + carIdToDelete + " do not exists"
                throw new System.Exception("Delete failed : Car with id: " + carIdToDelete + " do not exists");
            }
            // Delete the found car list index
            _cars.RemoveAt(carIndex);

            //Find all exsiting CarRental items of the deleted Car
            List<CarRent> carRentsToDelete = _carRentals.FindAll(carRental => carRental.Car.CarId == carIdToDelete);

            //Delete all exsiting CarRental items of the deleted Car
            _carRentals = _carRentals.FindAll(carRentalA => !carRentsToDelete.Any(carRentalB => carRentalA.RentalId == carRentalB.RentalId));

        }
        public void Update(Car carToUpdate)
        {
            int index = _cars.FindIndex(car => car.CarId == carToUpdate.CarId);
            if (index < 0) {

                //write to log : "Update failed : Car with id: " + carToUpdate.CarId + " do not exists"
                throw new System.Exception("Update failed : Car with id: " + carToUpdate.CarId + " do not exists");
            }
            _cars[index] = carToUpdate;
 
        }
    }
}
