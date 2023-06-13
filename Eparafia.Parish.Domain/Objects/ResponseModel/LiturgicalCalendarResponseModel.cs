using System.Runtime.InteropServices.JavaScript;
using Eparafia.Domain.Enums;

namespace Eparafia.Domain.Objects.ResponseModel;

public class LiturgicalCalendarResponseModel
{
    public DateTime Date { get; set; }
    public string Season { get; set; }
    public int Season_Week { get; set; }
    public IReadOnlyCollection<Celebration> Celebrations { get; set; }
}
