using Eparafia.Domain.Entities;
using Eparafia.Domain.Enums;

namespace Eparafia.Domain.DTOs;

public class IntentionDTO
{
    public DateTime Date { get; set; }
    public string Content { get; set; }
    public IntentionType Type { get; set; }
    public bool AutomaticAllocation { get; set; }
    public bool IsNovena { get; set; }
    public Guid Id { get; set; }

    public static IntentionDTO FromEntity(Intention intention)
    {
        if (intention is null)
            return null;
        return new IntentionDTO
        {
            Id = intention.Id,
            Date = intention.Date,
            Content = intention.Content,
            Type = intention.Type,
            AutomaticAllocation = intention.AutomaticAllocation,
            IsNovena = intention.IsNovena
        };
    }
}