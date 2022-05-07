using eparafia.Calendar.Event;
using eparafia.Helpers;

namespace eparafia.Calendar.RequestModel;

public class AddSpecialEventRequestModel : Request
{
    public AddSpecialEventRequestModel(int parafiaId, SpecialEvent specialEvent, string token) : base(token)
    {
        ParafiaId = parafiaId;
        SpecialEvent = specialEvent ?? throw new ArgumentNullException(nameof(specialEvent));
    }

    public int ParafiaId { get; }
    public SpecialEvent SpecialEvent { get; }
}