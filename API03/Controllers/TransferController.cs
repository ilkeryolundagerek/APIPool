using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private HttpClient _client;
        public TransferController(IHttpClientFactory factory)
        {
            _client=factory.CreateClient();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string base_address = "https://localhost:7170/api/values";
            return Ok(await _client.GetStringAsync(base_address));
        }
    }
}
