using Microsoft.AspNetCore.Mvc;
using SettlementApiService.Exceptions;
using SettlementApiService.Models;
using SettlementApiService.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SettlementApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettlementBookingsController : ControllerBase
    {
        private readonly ISettlementService settlementService;

        public SettlementBookingsController(ISettlementService settlementService)
        {
            this.settlementService = settlementService;
        }

        // GET: api/<SettlementBookingsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SettlementBookingsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SettlementBookingsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SettlementBookingModel model, CancellationToken cancellationToken)
        {
            try
            {
                var newid =  await settlementService.CreateSettlementBookingAsync(model, cancellationToken);

                return Ok(newid);
            }
            catch (BadRequestException)
            {
                return BadRequest();
            }
            catch(ConflictException)
            {
                return Conflict();
            }
        }

        // PUT api/<SettlementBookingsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SettlementBookingsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
