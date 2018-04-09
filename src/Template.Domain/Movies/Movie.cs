using Template.Domain.Customers;
using Template.Infrastructure;

namespace Template.Domain.Movies
{
    public abstract class Movie : Entity
    {
        public virtual string Name { get; protected set; }
        protected virtual int LicensingModel { get; set; }

        public abstract ExpirationDate GetExpirationDate();

        public virtual Dollars CalculatePrice(CustomerStatus status)
        {
            decimal modifier = 1 - status.GetDiscount();
            return GetBasePrice() * modifier;
        }

        protected abstract Dollars GetBasePrice();
    }
}
