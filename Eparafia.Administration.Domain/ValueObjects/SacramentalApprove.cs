using MassTransit.Futures.Contracts;

namespace Eparafia.Administration.Domain.ValueObjects;

public class SacramentalApprove : ValueObject
{
    public bool Confirmation { get; set; } //Bierzmowanie
    public bool Wedding { get; set; }
    public bool HigherOrdination { get; set; }
    public bool ReligiousVows {get ;set; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Confirmation;
        yield return Wedding;
        yield return HigherOrdination;
        yield return ReligiousVows;
    }
}