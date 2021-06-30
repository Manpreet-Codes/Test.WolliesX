using Answers.Services.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Answers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class SortController : ControllerBase
    {
        private readonly ISortService _sortService;

        public SortController(ISortService sortService)
        {
            _sortService = sortService;
        }

        [HttpGet]
        public IActionResult Get(string sortoption)
        {
            try
            {
                var response = Task.Run(async () => await _sortService.PerformSort(sortoption)).GetAwaiter().GetResult();
                return Ok(response);
            }
            catch (HttpRequestException exp)
            {
                return BadRequest(exp.Message);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}