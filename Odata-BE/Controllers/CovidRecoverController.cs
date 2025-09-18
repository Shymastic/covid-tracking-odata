using Odata_BE.Models;
using Odata_BE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace Odata_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CovidRecoverController : ControllerBase
    {
        private readonly CovidRecoverService _recoverService;

        public CovidRecoverController(CovidRecoverService recoverService)
        {
            _recoverService = recoverService;
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IQueryable<CovidRecoverCase>>> Get()
        {
            var cases = await _recoverService.GetRecoverCasesAsync();
            return Ok(cases.AsQueryable());
        }

    }
}
