using System;
using CityAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CityAPI.Data
{
	public class CityAPIDbContext : DbContext

	{
        public CityAPIDbContext(DbContextOptions options) : base(options)
        {

        }
        //the props stored here act as tables that are interacted with through the models folder
        //the "cities" property is directly linked to the city class in the models folder

        public DbSet<City> Cities { get; set; }

    }
}

