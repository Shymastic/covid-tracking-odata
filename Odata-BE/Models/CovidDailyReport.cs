﻿using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Odata_BE.Models
{
    public class CovidDailyReport
    {
        [Key]
        [Name("UID")]
        public double UID { get; set; } 

        [Name("Province_State")]
        public string? ProvinceState { get; set; }

        [Name("Country_Region")]
        public string? CountryRegion { get; set; }

        [Name("Last_Update")]
        public DateTime? LastUpdate { get; set; }

        [Name("Lat")]
        public double? Lat { get; set; }

        [Name("Long_")]
        public double? Long { get; set; }

        [Name("Confirmed")]
        public double? Confirmed { get; set; }

        [Name("Deaths")]
        public double? Deaths { get; set; }

        [Name("Recovered")]
        public double? Recovered { get; set; }

        [Name("Active")]
        public double? Active { get; set; }

        [Name("FIPS")]
        public double? FIPS { get; set; }

        [Name("Incident_Rate")]
        public double? IncidentRate { get; set; }

        [Name("Case_Fatality_Ratio")]
        public double? CaseFatalityRatio { get; set; }

        [Name("ISO3")]
        public string? ISO3 { get; set; }

    }
}
