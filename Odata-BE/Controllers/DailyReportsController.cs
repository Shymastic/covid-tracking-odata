﻿using Odata_BE.Models;
using Odata_BE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Globalization;

namespace Odata_BE.Controllers
{
    public class CovidDailyReportsController : ODataController
    {
        private readonly DailyReportService _dailyReportService;
        private readonly ILogger<CovidDailyReportsController> _logger;

        public CovidDailyReportsController(DailyReportService dailyReportService, ILogger<CovidDailyReportsController> logger)
        {
            _dailyReportService = dailyReportService;
            _logger = logger;
        }

        [EnableQuery]
        public async Task<ActionResult<IQueryable<CovidDailyReport>>> Get([FromQuery] string date)
        {
            if (!DateOnly.TryParse(date, CultureInfo.InvariantCulture, out var parsedDate))
            {
                return BadRequest("Định dạng ngày không hợp lệ. Vui lòng dùng 'yyyy-MM-dd'.");
            }

            _logger.LogInformation($"Fetching daily report for date: {parsedDate:yyyy-MM-dd}");

            var report = await _dailyReportService.GetDailyReportAsync(parsedDate);
            return Ok(report.AsQueryable());
        }
    }
}
