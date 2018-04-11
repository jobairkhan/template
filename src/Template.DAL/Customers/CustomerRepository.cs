using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Template.Domain.Customers;
using Template.Infrastructure;

namespace Template.DAL.Customers
{
    public class CustomerRepository : Repository<Customer>
    {
        private readonly ILogger<CustomerRepository> _log;
        private ApiSettings _apiSettings;

        public CustomerRepository(IUnitOfWork unitOfWork, 
                                  ILoggerFactory logger,
                                  IOptions<ApiSettings> entaSettingOptions)
            : base(unitOfWork)
        {
            Debug.Assert(logger != null, $"{nameof(logger)} != null");
            _log = logger.CreateLogger<CustomerRepository>();
            _apiSettings = entaSettingOptions?.Value ?? throw new ArgumentNullException(nameof(entaSettingOptions));
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
