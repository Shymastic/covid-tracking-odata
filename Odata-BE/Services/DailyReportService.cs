using Odata_BE.Models;
using CsvHelper;
using System.Globalization;
using System.Net;
namespace Odata_BE.Services
{

    public class DailyReportService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_daily_reports_us/";

        public DailyReportService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CovidDailyReport>> GetDailyReportAsync(DateOnly date)
        {
            string formattedDate = date.ToString("MM-dd-yyyy");
            string requestUrl = $"{BaseUrl}{formattedDate}.csv";

            try
            {
                var response = await _httpClient.GetAsync(requestUrl);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return Enumerable.Empty<CovidDailyReport>();
                }
                response.EnsureSuccessStatusCode();

                using var stream = await response.Content.ReadAsStreamAsync();
                using var reader = new StreamReader(stream);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                var records = csv.GetRecords<CovidDailyReport>().ToList();
                return records;
            }
            catch (HttpRequestException)
            {
                return Enumerable.Empty<CovidDailyReport>();
            }
        }
    }
}
