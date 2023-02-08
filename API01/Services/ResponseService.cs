using API01.Data.Repositories;
using API01.Entities;
using API01.Models.Responses;
using System.Linq.Expressions;
using Toolbox;

namespace API01.Services
{
    public interface IResponseService
    {
        Paging<PeopleResponseModel> GetPeopleWithPaging(int page, int page_size, Expression<Func<Person, bool>>? expression = null);
    }

    public class ResponseService : IResponseService
    {
        private IUnitOfWorkData _uowData;
        public ResponseService(IUnitOfWorkData uowData)
        {
            _uowData=uowData;
        }

        public Paging<PeopleResponseModel> GetPeopleWithPaging(int page, int page_size, Expression<Func<Person, bool>>? expression = null)
        {
            var data = from d in _uowData.personRepository.ReadMany(expression)
                       select new PeopleResponseModel
                       {
                           Id = d.Id,
                           Firstname=d.Firstname,
                           Middlename = d.Middlename,
                           Lastname=d.Lastname,
                           DepartmentId=d.DepartmentId,
                           Active= d.Active,
                           Deleted= d.Deleted,
                           Email=d.Email,
                           DepartmentName = d.Department.Title
                       };

            return new Paging<PeopleResponseModel>(data, page, page_size);
        }
    }
}
