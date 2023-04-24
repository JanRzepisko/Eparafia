
namespace Eparafia.Administration.Domain.ValueObjects;

public class BaptismClient : ValueObject
{
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateOnly BaptismDate { get; set; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return Surname;
        yield return BirthDate;
        yield return BaptismDate;
    }
}