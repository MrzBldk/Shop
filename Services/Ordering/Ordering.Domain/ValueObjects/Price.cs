namespace Ordering.Domain.ValueObjects
{
    public class Price
    {
        private decimal _value;

        public Price(decimal value)
        {
            _value = value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public static implicit operator decimal(Price value)
        {
            return value._value;
        }

        public static Price operator -(Price value)
        {
            return new Price(Math.Abs(value._value) * -1);
        }

        public static implicit operator Price(decimal value)
        {
            return new Price(value);
        }

        public static Price operator +(Price price1, Price price2)
        {
            return new Price(price1._value + price2._value);
        }

        public static Price operator -(Price price1, Price price2)
        {
            return new Price(price1._value - price2._value);
        }

        public static bool operator <(Price price1, Price price2)
        {
            return price1._value < price2._value;
        }

        public static bool operator >(Price price1, Price price2)
        {
            return price1._value > price2._value;
        }

        public static bool operator <=(Price price1, Price price2)
        {
            return price1._value <= price2._value;
        }

        public static bool operator >=(Price price1, Price price2)
        {
            return price1._value >= price2._value;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj is decimal)
                return (decimal)obj == _value;

            return ((Price)obj)._value == _value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}