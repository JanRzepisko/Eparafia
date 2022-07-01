using eparafia.Helpers;

namespace eparafia.Intention.RequestModel;

public class AddNovenaRequestModel : Request
{
     public AddNovenaRequestModel(string? token, int parafiaId, string startDate, string value, int count) : base(token)
     {
          ParafiaId = parafiaId;
          StartDate = startDate;
          Value = value;
          Count = count;
     }

     public int ParafiaId { get; }
     public int Count { get; }
     public string StartDate { get; }
     public string Value { get; }
}