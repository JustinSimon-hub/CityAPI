﻿using System;
namespace CityAPI.Models
{
    public class AddCityRequest
    {
        public string CityName { get; set; }
        public string County { get; set; }
        public long ZipCode { get; set; }
    }
}
