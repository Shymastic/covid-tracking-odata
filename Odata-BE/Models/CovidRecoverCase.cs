﻿using System.ComponentModel.DataAnnotations;

namespace Odata_BE.Models
{
    public class CovidRecoverCase
    {
        [Key]
        public Guid Id { get; set; }
        public string? ProvinceState { get; set; }
        public string CountryRegion { get; set; }
        public double? Lat { get; set; }
        public double? Long { get; set; }
        public DateTime Date { get; set; }

        public int Recovered { get; set; }
    }
}
