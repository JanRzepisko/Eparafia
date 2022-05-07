using eparafia.Calendar.EventEnums;
using eparafia.Helpers;

namespace eparafia.Calendar.RequestModel;

public class EditSpecialEventRequestModel : Request
{
    public EditSpecialEventRequestModel(int eventId, string token, object value, EditType editType) : base(token)
    {
        EventId = eventId;
        Value = value;
        EditType = editType;
    }

    public int EventId { get; }
    public EditType EditType { get; }
    public object Value { get; }
}