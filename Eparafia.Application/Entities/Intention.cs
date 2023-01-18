using Eparafia.Application.DataAccess.Abstract;
using Eparafia.Application.Enums;

namespace Eparafia.Application.Entities;

public class Intention : Entity
{
    public DateTime Date { get; set; }
    public string Content { get; set; }
    public IntentionType Type { get; set; }
    public bool AutomaticAllocation { get; set; }
    public bool IsNovena { get; set; }
    public bool IsVerified { get; set; }
    public Guid ParishId { get; set; }
    public Parish Parish { get; set; }
}