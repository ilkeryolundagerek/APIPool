using API01.Data.Repositories;
using API01.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IUnitOfWorkData _uow;

        public ValuesController(IUnitOfWorkData uow)
        {
            _uow=uow;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return _uow.personRepository.ReadMany();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Person Get(int id)
        {
            return _uow.personRepository.ReadOneByKey(id);
        }
    }
}
