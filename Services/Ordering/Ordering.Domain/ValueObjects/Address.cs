namespace Ordering.Domain.ValueObjects
{
    public class Address
    {
        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string Country { get; }
        public string ZipCode { get; }

        public Address(string street, string city, string state, string country, string zipCode)
        {
            Street = !string.IsNullOrWhiteSpace(street) ? street : throw new DomainException($"Invalid {nameof(street)}");
            City = !string.IsNullOrWhiteSpace(city) ? city : throw new DomainException($"Invalid {nameof(city)}");
            State = !string.IsNullOrWhiteSpace(state) ? state : throw new DomainException($"Invalid {nameof(state)}");
            Country = !string.IsNullOrWhiteSpace(country) ? country : throw new DomainException($"Invalid {nameof(country)}");
            ZipCode = !string.IsNullOrWhiteSpace(zipCode) ? zipCode : throw new DomainException($"Invalid {nameof(zipCode)}");
        }

        public override string ToString()
        {
            return $"{Country}, {State}, {City}, {Street}, {ZipCode}";
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            
            if (obj is not Address other)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return Street == other.Street &&
                    City == other.City &&
                    State == other.State &&
                    Country == other.Country &&
                    ZipCode == other.ZipCode;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Street, City, State, Country, ZipCode);
        }
    }
}
