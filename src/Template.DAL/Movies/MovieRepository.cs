using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Logging;
using Template.Domain.Movies;
using Template.Infrastructure;

namespace Template.DAL.Movies
{
    public class MovieRepository : Repository<Movie>
    {
        private readonly ILogger<MovieRepository> _log;

        public MovieRepository(IUnitOfWork unitOfWork, ILoggerFactory logger)
            : base(unitOfWork)
        {
            _log = logger.CreateLogger<MovieRepository>();
        }

        public IReadOnlyList<Movie> GetList(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _log.LogInformation("Get all customers");

            return _unitOfWork.Query<Movie>().ToList();
        }
    }
}
