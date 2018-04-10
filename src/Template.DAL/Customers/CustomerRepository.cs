using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Template.Domain.Customers;
using Template.Infrastructure;

namespace Template.DAL.Customers
{
    public class CustomerRepository : Repository<Customer>
    {
        private readonly ILogger<CustomerRepository> _log;

        public CustomerRepository(IUnitOfWork unitOfWork, ILoggerFactory logger)
            : base(unitOfWork)
        {
            _log = logger.CreateLogger<CustomerRepository>();
        }

        public async Task<IReadOnlyList<Customer>> GetList(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            _log.LogInformation("Get all customers");

            return await 
                _unitOfWork
                .Query<Customer>()
                .ToListAsync(cancellationToken);
        }

        public  async Task<Customer> GetByEmail(Email email,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            _log.LogInformation($"Get by email {email}");

            return await 
                _unitOfWork
                .Query<Customer>()
                .SingleOrDefaultAsync(
                        x => x.Email == email.Value, 
                        cancellationToken);
        }
    }
}
