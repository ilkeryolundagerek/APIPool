using API01.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Toolbox;
using Toolbox.DataTools;

namespace API01.Data.Repositories
{
    public interface IPersonRepository : IGenericRepository<Person>
    {

    }
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(DbContext context) : base(context)
        {
        }

        public override IEnumerable<Person> ReadMany(Expression<Func<Person, bool>>? expression = null)
        {
            var data = _set.Include(x => x.Department).Include(x => x.Addresses);
            return expression != null ? data.Where(expression) : data;
        }

        public override Paging<Person> ReadManyWithPaging(int page, int page_size, Expression<Func<Person, bool>>? expression = null)
        {
            var included = _set.Include(x => x.Department).Include(x => x.Addresses);
            var data = expression != null ? included.Where(expression) : included;
            return new Paging<Person>(data, page, page_size);
        }
    }

    public interface IAddressRepository : IGenericRepository<Address>
    {

    }
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(DbContext context) : base(context)
        {
        }
    }

    public interface IDepartmentRepository : IGenericRepository<Department>
    {

    }
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(DbContext context) : base(context)
        {
        }
    }

    public interface IUnitOfWorkData : IDisposable
    {
        IPersonRepository personRepository { get; }
        IAddressRepository addressRepository { get; }
        IDepartmentRepository departmentRepository { get; }

        void Commit();
        Task CommitAsync();
    }

    public class UnitOfWorkData : IUnitOfWorkData
    {
        private IPersonRepository _personRepository;
        private IAddressRepository _addressRepository;
        private IDepartmentRepository _departmentRepository;
        private DataContext _context;

        public UnitOfWorkData(DataContext context)
        {
            _context = context;
        }
        public IPersonRepository personRepository => _personRepository ??= new PersonRepository(_context);

        public IAddressRepository addressRepository => _addressRepository ??= new AddressRepository(_context);

        public IDepartmentRepository departmentRepository => _departmentRepository ??= new DepartmentRepository(_context);

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                Dispose();
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
