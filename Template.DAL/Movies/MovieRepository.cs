using System.Collections.Generic;
using System.Linq;
using Template.Domain.Movies;
using Template.Infrastructure;

namespace Template.DAL.Movies
{
    public class MovieRepository : Repository<Movie>
    {
        public MovieRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IReadOnlyList<Movie> GetList()
        {
            return _unitOfWork.Query<Movie>().ToList();
        }
    }
}
