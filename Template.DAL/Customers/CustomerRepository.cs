using System.Collections.Generic;
using System.Linq;
using Template.Domain.Customers;
using Template.Infrastructure;

namespace Template.DAL.Customers
{
    public class CustomerRepository : Repository<Customer>
    {
        public CustomerRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IReadOnlyList<Customer> GetList()
        {
            return _unitOfWork
                .Query<Customer>()
                .ToList();
        }

        public Customer GetByEmail(Email email)
        {
            return _unitOfWork
                .Query<Customer>()
                .SingleOrDefault(x => x.Email == email.Value);
        }
    }
}
