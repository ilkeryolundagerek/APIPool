using API01.Data.Repositories;
using API01.Entities;
using Microsoft.AspNetCore.Mvc;
using Toolbox;

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
        public Paging<Person> Get(int page = 1, int page_size = 20)
        {
            //var data = _uow.personRepository.ReadMany(x => x.DepartmentId==25);
            //return new Paging<Person>(data, page, page_size);
            var data = _uow.personRepository.ReadManyWithPaging(page, page_size);
            return data;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Person Get(int id)
        {
            return _uow.personRepository.ReadOneByKey(id);
        }

        [HttpDelete]
        public Person Delete(int id)
        {
            var entity = _uow.personRepository.ReadOneByKey(id);
            if (entity != null)
            {
                entity.Deleted = true;
                _uow.Commit();
            }
            return entity;
        }
    }
}
