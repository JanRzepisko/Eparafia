using eparafia.Calendar.Event;
using eparafia.Helpers;

namespace eparafia.Calendar.RequestModel;

public class EditDefaultEventRequestModel : Request
{
    public EditDefaultEventRequestModel(int parafiaId, List<DefaultEvent> defaultWeek, string token) : base(token)
    {
        ParafiaId = parafiaId;
        DefaultWeek = defaultWeek;
    }

    public int ParafiaId { get; }
    public List<DefaultEvent> DefaultWeek { get; }
}