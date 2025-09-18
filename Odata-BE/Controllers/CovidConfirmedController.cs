using Odata_BE.Models;
using Odata_BE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace Odata_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CovidConfirmedController : ControllerBase
    {
        private readonly CovidConfirmService _confirmService;

        public CovidConfirmedController(CovidConfirmService covidDataService)
        {
            _confirmService = covidDataService;
        }

        [EnableQuery(PageSize = 100)]
        [HttpGet]
        public async Task<ActionResult<IQueryable<CovidConfirmedCase>>> Get()
        {
            var cases = await _confirmService.GetConfirmedCasesAsync();
            return Ok(cases.AsQueryable());
        }
    }

}
