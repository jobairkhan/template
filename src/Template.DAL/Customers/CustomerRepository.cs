using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Customers;
using Template.Infrastructure;

namespace Template.DAL.Customers
{
    public class CustomerRepository : Repository<Customer>
    {
        public CustomerRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public async Task<IReadOnlyList<Customer>> GetList(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await 
                _unitOfWork
                .Query<Customer>()
                .ToListAsync(cancellationToken);
        }

        public  async Task<Customer> GetByEmail(Email email,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await 
                _unitOfWork
                .Query<Customer>()
                .SingleOrDefaultAsync(
                        x => x.Email == email.Value, 
                        cancellationToken);
        }
    }
}
