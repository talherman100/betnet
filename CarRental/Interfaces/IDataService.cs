using CarRental.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CarRental.Interfaces
{
    public interface IDataService
    {
        void Add(Car car);
        void Update(Car car);
        void Delete(int carId);
        List<Car> GetFilteredCars(Search search);
    }
}
