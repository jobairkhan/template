using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Template.Api.Utils;
using Template.DAL;
using Template.DAL.Customers;
using Template.DAL.Movies;
using Template.Domain.Customers;
using Template.Infrastructure;

namespace Template.Api.Customers
{
    /// <summary>
    /// Customers Controller
    /// </summary>
    [Route("api/[controller]")]
    public class CustomersController : BaseController
    {
        private readonly MovieRepository _movieRepository;
        private readonly CustomerRepository _customerRepository;

        /// <summary>
        /// Create an instance of customer controller
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="movieRepository"></param>
        /// <param name="customerRepository"></param>
        public CustomersController(IUnitOfWork unitOfWork, MovieRepository movieRepository, CustomerRepository customerRepository)
            : base(unitOfWork)
        {
            _customerRepository = customerRepository;
            _movieRepository = movieRepository;
        }

        /// <summary>
        /// Get customer information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetById(id, cancellationToken);
            if (customer == null)
                return NotFound();

            var dto = new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name.Value,
                Email = customer.Email.Value,
                MoneySpent = customer.MoneySpent,
                Status = customer.Status.Type.ToString(),
                StatusExpirationDate = customer.Status.ExpirationDate,
                PurchasedMovies = customer.PurchasedMovies.Select(x => new PurchasedMovieDto
                {
                    Price = x.Price,
                    ExpirationDate = x.ExpirationDate,
                    PurchaseDate = x.PurchaseDate,
                    Movie = new MovieDto
                    {
                        Id = x.Movie.Id,
                        Name = x.Movie.Name
                    }
                }).ToList()
            };

            return Ok(dto);
        }

        /// <summary>
        /// List of customers
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult>  GetList(CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetList(cancellationToken);

            var data = customers.Select(x => new CustomerInListDto
            {
                Id = x.Id,
                Name = x.Name.Value,
                Email = x.Email.Value,
                MoneySpent = x.MoneySpent,
                Status = x.Status.Type.ToString(),
                StatusExpirationDate = x.Status.ExpirationDate
            }).ToList();

            return Ok(data);
        }

        /// <summary>
        /// Add a new customer
        /// </summary>
        /// <param name="item"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult>  Create([FromBody] CreateCustomerDto item, CancellationToken cancellationToken)
        {
            Result<CustomerName> customerNameOrError = CustomerName.Create(item.Name);
            Result<Email> emailOrError = Email.Create(item.Email);

            Result result = Result.Combine(customerNameOrError, emailOrError);
            if (result.IsFailure)
                return Error(result.Error);

            var existingCustomer = await _customerRepository.GetByEmail(emailOrError.Value, cancellationToken);
            if (existingCustomer != null)
                return Error("Email is already in use: " + item.Email);

            var customer = new Customer(customerNameOrError.Value, emailOrError.Value);
            _customerRepository.Add(customer);

            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateCustomerDto item, CancellationToken cancellationToken)
        {
            Result<CustomerName> customerNameOrError = CustomerName.Create(item.Name);
            if (customerNameOrError.IsFailure)
                return Error(customerNameOrError.Error);

            var customer = await _customerRepository.GetById(id, cancellationToken);
            if (customer == null)
                return Error("Invalid customer id: " + id);

            customer.Name = customerNameOrError.Value;

            return Ok();
        }

        /// <summary>
        /// To purchase a movie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movieId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id}/movies")]
        public async Task<IActionResult> PurchaseMovie(long id, [FromBody] long movieId, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetById(movieId, cancellationToken);
            if (movie == null)
                return Error("Invalid movie id: " + movieId);

            var customer = await _customerRepository.GetById(id, cancellationToken);
            if (customer == null)
                return Error("Invalid customer id: " + id);

            if (customer.HasPurchasedMovie(movie))
                return Error("The movie is already purchased: " + movie.Name);

            customer.PurchaseMovie(movie);

            return Ok();
        }

        /// <summary>
        /// To promote a customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id}/promotion")]
        public async Task<IActionResult> PromoteCustomer(long id, CancellationToken cancellationToken)
        {
            Customer customer = await _customerRepository.GetById(id, cancellationToken);
            if (customer == null)
                return Error("Invalid customer id: " + id);

            Result promotionCheck = customer.CanPromote();
            if (promotionCheck.IsFailure)
                return Error(promotionCheck.Error);

            customer.Promote();

            return Ok();
        }
    }
}
