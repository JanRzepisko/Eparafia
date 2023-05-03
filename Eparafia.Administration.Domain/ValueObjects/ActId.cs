namespace Eparafia.Administration.Domain.ValueObjects;

public class ActId : ValueObject
{
    public string Id { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}