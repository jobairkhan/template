using System;
using Template.Domain.Customers;

namespace Template.Domain.Movies
{
    public class TwoDaysMovie : Movie
    {
        public override ExpirationDate GetExpirationDate()
        {
            return (ExpirationDate)DateTime.UtcNow.AddDays(2);
        }

        protected override Dollars GetBasePrice()
        {
            return Dollars.Of(4);
        }
    }
}