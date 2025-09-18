﻿using Odata_BE.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Formats.Asn1;
using System.Globalization;
using System.Net.Http;

namespace Odata_BE.Services
{

    public class CovidConfirmService
    {
        private static readonly List<CovidConfirmedCase> _cache = new List<CovidConfirmedCase>();
        private static DateTime _lastFetchTime = DateTime.MinValue;
        private readonly HttpClient _httpClient;
        private const string Covid_Confirm = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";


        public CovidConfirmService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CovidConfirmedCase>> GetConfirmedCasesAsync()
        {
            if (_cache.Any() && (DateTime.UtcNow - _lastFetchTime).TotalHours < 1)
            {
                return _cache;
            }

            var records = new List<CovidConfirmedCase>();
            var response = await _httpClient.GetAsync(Covid_Confirm);
            response.EnsureSuccessStatusCode();

            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                var header = csv.HeaderRecord;

                var dateColumns = header.Skip(4).ToList();

                while (csv.Read())
                {
                    var provinceState = csv.GetField(0);
                    var countryRegion = csv.GetField(1);
                    var lat = csv.GetField<double?>(2);
                    var lon = csv.GetField<double?>(3);

                    foreach (var dateCol in dateColumns)
                    {
                        var confirmed = csv.GetField<int>(dateCol);

                        records.Add(new CovidConfirmedCase
                        {
                            Id = Guid.NewGuid(),
                            ProvinceState = provinceState,
                            CountryRegion = countryRegion,
                            Lat = lat,
                            Long = lon,
                            Date = DateTime.Parse(dateCol, CultureInfo.InvariantCulture),
                            Confirmed = confirmed
                        });
                    }
                }
            }

            _cache.Clear();
            _cache.AddRange(records);
            _lastFetchTime = DateTime.UtcNow;

            return _cache;
        }
    }
}
