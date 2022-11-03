namespace Ordering.Domain.ValueObjects
{
    public class Address
    {
        private string _street;
        private string _city;
        private string _state;
        private string _country;
        private string _zipCode;

        public Address(string street, string city, string state, string country, string zipCode)
        {
            _street = !string.IsNullOrWhiteSpace(street) ? street : throw new DomainException($"Invalid {nameof(street)}");
            _city = !string.IsNullOrWhiteSpace(city) ? city : throw new DomainException($"Invalid {nameof(city)}");
            _state = !string.IsNullOrWhiteSpace(state) ? state : throw new DomainException($"Invalid {nameof(state)}");
            _country = !string.IsNullOrWhiteSpace(country) ? country : throw new DomainException($"Invalid {nameof(country)}");
            _zipCode = !string.IsNullOrWhiteSpace(zipCode) ? zipCode : throw new DomainException($"Invalid {nameof(zipCode)}");
        }

        public string GetStreet()
        {
            return _street;
        }

        public string GetCity()
        {
            return _city;
        }

        public string GetState()
        {
            return _state;
        }

        public string GetCountry()
        {
            return _country;
        }

        public string GetZipCode()
        {
            return _zipCode;
        }

        public override string ToString()
        {
            return $"{_country}. {_state}, {_city}, {_street}, {_zipCode}";
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            
            if (obj is not Address other)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return _street == other._street &&
                    _city == other._city &&
                    _state == other._state &&
                    _country == other._country &&
                    _zipCode == other._zipCode;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_street, _city, _state, _country, _zipCode);
        }
    }
}
