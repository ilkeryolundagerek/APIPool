using API04.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyController : ControllerBase
    {
        private HttpClient _httpClient;

        public DummyController(IHttpClientFactory factory)
        {
            _httpClient=factory.CreateClient("dummyapi.io");
        }

        // GET: api/<DummyController>
        [HttpGet]
        public async Task<IActionResult> Get(int limit = 20, int page = 0)
        {
            string endpoint = $"user?page={page}&limit={limit}";

            return Ok(JsonConvert.DeserializeObject<UserList>(await _httpClient.GetStringAsync(endpoint)));
        }

        // GET api/<DummyController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            string endpoint = $"user/{id}";
            return Ok(JsonConvert.DeserializeObject<UserFull>(await _httpClient.GetStringAsync(endpoint)));
        }

        // POST api/<DummyController>
        [HttpPost]
        public async Task Post([FromBody] string firstName, [FromBody] string lastName, [FromBody] string email)
        {
            string endpoint = $"/user/create";
            HttpContent payload = new StringContent("");
            var response = await _httpClient.PostAsync(endpoint, payload);
        }

        // PUT api/<DummyController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DummyController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
