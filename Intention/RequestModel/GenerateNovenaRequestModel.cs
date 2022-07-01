using eparafia.Helpers;

namespace eparafia.Intention.RequestModel;

public class GenerateNovenaRequestModel : Request
{
     public GenerateNovenaRequestModel(string? token, int parafiaId, int week) : base(token)
     {
          ParafiaId = parafiaId;
          Week = week;
     }

     public int ParafiaId { get; }
     public int Week { get; }
}