using Eparafia.Administration.Domain.Enums;

namespace Eparafia.Administration.Domain.ValueObjects;

public class Person : ValueObject
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string CityOfBirth { get; set; }
    public Confession Confession { get; set; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
        yield return DateOfBirth;
        yield return CityOfBirth;
        yield return Confession;
    }
}