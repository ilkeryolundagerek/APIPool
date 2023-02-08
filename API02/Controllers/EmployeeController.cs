using API02.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private HttpClient _client;

        public EmployeeController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public async Task<ReqResRoot?> Get()
        {
            string response = await _client.GetStringAsync("https://reqres.in/api/users");
            DefaultContractResolver cr = new DefaultContractResolver()
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            return JsonConvert.DeserializeObject<ReqResRoot>(response, new JsonSerializerSettings { ContractResolver=cr, Formatting=Formatting.Indented });
        }

        [HttpGet("{id}")]
        public async Task<ReqResUser?> Get(int id)
        {
            string response = await _client.GetStringAsync($"https://reqres.in/api/users/{id}");
            DefaultContractResolver cr = new DefaultContractResolver()
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            var result = JsonConvert.DeserializeObject<ReqResUserRoot>(response, new JsonSerializerSettings { ContractResolver=cr, Formatting=Formatting.Indented });
            return result?.Data;
        }
    }
}
