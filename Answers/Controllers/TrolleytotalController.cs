using Answers.Models.Model;
using Answers.Services.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Answers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrolleytotalController : ControllerBase
    {
        private ITrolleyTotalService _trolleyTotalService;
        private static bool mailSent = false;

        public TrolleytotalController(ITrolleyTotalService trolleyTotalService)
        {
            _trolleyTotalService = trolleyTotalService;
        }

        [HttpPost]
        public IActionResult CalculateTotal([FromBody]Products products)
        {
            try
            {
                var response = Task.Run(async () => await _trolleyTotalService.CalculateTotal(products)).GetAwaiter().GetResult();
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