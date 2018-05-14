using System;
using Template.Domain.Movies;
using Template.Infrastructure;

namespace Template.Domain.Customers
{
    public class PurchasedMovie : Entity
    {
        public Movie Movie { get; protected set; }
        public Customer Customer { get; protected set; }

        private decimal _price;
        public Dollars Price
        {
            get => Dollars.Of(_price);
            protected set => _price = value;
        }

        public DateTime PurchaseDate { get; protected set; }

        private DateTime? _expirationDate;
        public ExpirationDate ExpirationDate
        {
            get => (ExpirationDate)_expirationDate;
            protected set => _expirationDate = value;
        }

        protected PurchasedMovie()
        {
        }

        internal PurchasedMovie(Movie movie, Customer customer, Dollars price, ExpirationDate expirationDate)
        {
            if (price == null || price.IsZero)
                throw new ArgumentException(nameof(price));
            if (expirationDate == null || expirationDate.IsExpired)
                throw new ArgumentException(nameof(expirationDate));

            Movie = movie ?? throw new ArgumentNullException(nameof(movie));
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            Price = price;
            ExpirationDate = expirationDate;
            PurchaseDate = DateTime.UtcNow;
        }
    }
}
