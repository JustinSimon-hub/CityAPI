using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityAPI.Data;
using CityAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CityAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class CityController : Controller
    {
        //talks to the inmemory database

        private readonly CityAPIDbContext dbContext;

        public CityController(CityAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        //retrieve single resource
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetCity([FromRoute] Guid id)
        {
            var city = await dbContext.Cities.FindAsync(id);

            if(city == null)
            {
                return NotFound("Doesnt exist in db");
            }
            return Ok(city);
        }





        [HttpGet]
        public async Task<IActionResult> GetAllCity()
        {
            //talking to the database inside the data folder

           return Ok(await dbContext.Cities.ToListAsync());

        }


        //add city functionality
        //post corresponds to creating new resources

        [HttpPost]
        public async Task<IActionResult> AddCity(AddCityRequest addCityRequest)
        {
            //mapping between the addcity request to the domain model

            var city = new City()
            {
                Id = Guid.NewGuid(),
                CityName = addCityRequest.CityName,
                County = addCityRequest.County,
                ZipCode = addCityRequest.ZipCode,

            };
            //to use async the await modifer has to be used
           await dbContext.Cities.AddAsync(city);
            await dbContext.SaveChangesAsync();
            return Ok(city);
        }



        //update resources method
        [HttpPut]
        //specifying type guid makes it type safe
        //the name defined in the route must match the name inside the method paramter
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCity([FromRoute] Guid id, UpdateCityRequest updateCityRequest)
        {
            //checks if the database has the value that corresponnds with the guid(which acts as the primary key)
           var city = await dbContext.Cities.FindAsync(id);
            if (city != null)
            {
                city.CityName = updateCityRequest.CityName;
                city.County = updateCityRequest.County;
                city.ZipCode = updateCityRequest.ZipCode;

               await dbContext.SaveChangesAsync();
                return Ok(city);
            }
            else return NotFound();



        }




        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteCity([FromRoute] Guid id)
        {
          var city = await dbContext.Cities.FindAsync(id);

            if(city != null)
            {
                dbContext.Remove(city);
               await dbContext.SaveChangesAsync();
                return Ok($"{city.CityName} was deleted");
            }
            return NotFound();

        }
    }
}

