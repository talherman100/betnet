using CarRental.Interfaces;
using CarRental.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CarRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarRentalController : Controller
    {
        IDataService _dataService;
        private readonly string _errorMessage = "Error has occurred : ";
        public CarRentalController(IDataService dataService)
        {
            _dataService = dataService;
        }
        [HttpPost("get-filtered-cars")]
        public IActionResult GetFilteredCars(Search search)
        {
            try {
                var result = _dataService.GetFilteredCars(search);
                return Ok(result);
            }
            catch(Exception ex)
            {
                //log ex message
                return BadRequest(_errorMessage + ex.Message);
            }

        }
        [HttpPut("update")]
        public IActionResult Update(Car car)
        {
            try
            {
                _dataService.Update(car);
                return Ok();
            }
            catch (Exception ex)
            {
                //log ex message
                return BadRequest(_errorMessage + ex.Message);
            }

        }
        [HttpDelete("delete")]
        public IActionResult Delete(Car car)
        {
            try
            {
                _dataService.Delete(car.CarId);
                return Ok();
            }
            catch (Exception ex)
            {
                //log ex message
                return BadRequest(_errorMessage + ex.Message);
            }

        }
        [HttpPost("add")]
        public IActionResult Add(Car car)
        {
            try
            {
                _dataService.Add(car);
                return Ok();
            }
            catch (Exception ex)
            {
                //log ex message
                return BadRequest(_errorMessage + ex.Message);
            }

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
