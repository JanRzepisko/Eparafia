public abstract class ValueObject
{
    protected abstract IEnumerable<object> GetEqualityComponents();
    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (obj.GetType() != this.GetType())
        { 
            throw new ArgumentException($"Invalid comparison of Value Objects of different types: {GetType()} and {obj.GetType()}"); 
        }
        var valueObj = (ValueObject)obj;

        return this.GetEqualityComponents().SequenceEqual(valueObj.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents().Aggregate(1, (a, b) => HashCode.Combine(a, b));
    }

    public static bool operator ==(ValueObject a, ValueObject b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
        {
            return true;
        }
        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
        {
            return false;
        }
        return a.Equals(b);
    }
    public static bool operator !=(ValueObject a, ValueObject b)
    {
        return !(a == b);
    }
}