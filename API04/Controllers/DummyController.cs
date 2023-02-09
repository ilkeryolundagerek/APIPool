using API04.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Xml.Linq;

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
        public async Task<ActionResult<HttpResponseMessage>> Post(string fName, string lName, string mail)
        {
            string endpoint = $"user/create";
            var payload_obj = new UserCreate { firstName = fName, lastName = lName, email = mail };
            var payload_json = JsonConvert.SerializeObject(payload_obj);
            //application/json: Json formatında nesne gönderdiğimizi belirtir.
            HttpContent payload = new StringContent(payload_json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(endpoint, payload);

            return response;
        }

        // PUT api/<DummyController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<HttpResponseMessage>> Put(string id, string fName, string lName)
        {
            string endpoint = $"user/{id}";
            var payload_obj = new UserUpdate { firstName = fName, lastName = lName };
            var payload_json = JsonConvert.SerializeObject(payload_obj);
            //application/json: Json formatında nesne gönderdiğimizi belirtir.
            HttpContent payload = new StringContent(payload_json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(endpoint, payload);
            return response;
        }

        // DELETE api/<DummyController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            string endpoint = $"user/{id}";
            var response = await _httpClient.DeleteAsync(endpoint);
            return Ok(response.Content);
        }
    }
}
