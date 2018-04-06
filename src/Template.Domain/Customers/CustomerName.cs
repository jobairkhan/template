using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Template.Domain.Customers
{
    public class CustomerName : ValueObject
    {
        public string Value { get; }

        private CustomerName() { }

        private CustomerName(string value)
        {
            Value = value;
        }

        public static Result<CustomerName> Create(string customerName)
        {
            customerName = (customerName ?? string.Empty).Trim();

            if (customerName.Length == 0)
                return Result.Fail<CustomerName>("Customer name should not be empty");

            if (customerName.Length > 100)
                return Result.Fail<CustomerName>("Customer name is too long");

            return Result.Ok(new CustomerName(customerName));
        }
        
        public static implicit operator string(CustomerName customerName)
        {
            return customerName.Value;
        }

        public static explicit operator CustomerName(string customerName)
        {
            return Create(customerName).Value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
