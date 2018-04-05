﻿using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Template.Domain.Customers
{
    public class Dollars : ValueObject
    {
        private const decimal MaxDollarAmount = 1_000_000;

        public decimal Value { get; }

        public bool IsZero => Value == 0;

        private Dollars(decimal value)
        {
            Value = value;
        }

        public static Result<Dollars> Create(decimal dollarAmount)
        {
            if (dollarAmount < 0)
                return Result.Fail<Dollars>("Dollar amount cannot be negative");

            if (dollarAmount > MaxDollarAmount)
                return Result.Fail<Dollars>("Dollar amount cannot be greater than " + MaxDollarAmount);

            if (dollarAmount % 0.01m > 0)
                return Result.Fail<Dollars>("Dollar amount cannot contain part of a penny");

            return Result.Ok(new Dollars(dollarAmount));
        }

        public static Dollars Of(decimal dollarAmount)
        {
            return Create(dollarAmount).Value;
        }

        public static Dollars operator *(Dollars dollars, decimal multiplier)
        {
            return new Dollars(dollars.Value * multiplier);
        }

        public static Dollars operator +(Dollars dollars1, Dollars dollars2)
        {
            return new Dollars(dollars1.Value + dollars2.Value);
        }

        public static implicit operator decimal(Dollars dollars)
        {
            return dollars.Value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
