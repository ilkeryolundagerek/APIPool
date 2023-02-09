using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalcController : ControllerBase
    {
        [HttpGet]
        public double Get(double celcius)
        {
            return celcius*(9.0/5)+32;
        }
    }
}
