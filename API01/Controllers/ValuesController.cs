using API01.Data.Repositories;
using API01.Entities;
using API01.Models.Responses;
using API01.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Toolbox;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API01.Controllers
{
    [EnableCors("MyCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IUnitOfWorkData _uow;
        private IResponseService _service;
        public ValuesController(IUnitOfWorkData uow, IResponseService service)
        {
            _uow=uow;
            _service=service;
        }

        // GET: api/<ValuesController>
        [EnableCors("Values")]
        [HttpGet]
        public Paging<PeopleResponseModel> Get(int page = 1, int page_size = 20)
        {
            //var data = _uow.personRepository.ReadMany(x => x.DepartmentId==25);
            //return new Paging<Person>(data, page, page_size);
            //var data = _uow.personRepository.ReadManyWithPaging(page, page_size);
            //return data;
            return _service.GetPeopleWithPaging(page, page_size);
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
