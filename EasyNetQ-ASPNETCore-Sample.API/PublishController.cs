using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ_ASPNETCore_Sample.Worker.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyNetQ_ASPNETCore_Sample.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublishController : ControllerBase
    {
        private readonly IBus _bus;

        public PublishController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> PublishAsync([FromBody] SampleMessage message)
        {
            await _bus.PublishAsync(message, config =>
            {
                config.WithQueueName("sampleQueue");
            });

            return Ok(message);
        }
    }
}