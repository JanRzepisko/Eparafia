using eparafia.Helpers;

namespace eparafia.Intention.RequestModel;

public class AddIntentionRequestModel : Request
{
     public AddIntentionRequestModel(string? token, int parafiaId, string date, IntentionType type, string value) : base(token)
     {
          ParafiaId = parafiaId;
          Date = date;
          Type = type;
          Value = value;
     }

     public int ParafiaId { get; }
     public string Date { get; }
     public IntentionType Type { get; }
     public string Value { get; }
}