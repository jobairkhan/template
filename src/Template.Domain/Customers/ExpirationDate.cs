using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Template.Domain.Customers
{
    public class ExpirationDate : ValueObject
    {
        public static readonly ExpirationDate Infinite = new ExpirationDate(null);
        private DateTime? _date;

        public DateTime? Date => _date;

        public bool IsExpired => this != Infinite && Date < DateTime.UtcNow;

        private ExpirationDate() { }

        private ExpirationDate(DateTime? date)
        {
            _date = date;
        }

        public static Result<ExpirationDate> Create(DateTime date)
        {
            return Result.Ok(new ExpirationDate(date));
        }

        public static explicit operator ExpirationDate(DateTime? date)
        {
            if (date.HasValue)
                return Create(date.Value).Value;

            return Infinite;
        }

        public static implicit operator DateTime? (ExpirationDate date)
        {
            return date.Date;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Date;
        }
    }
}
