using Eparafia.Application.Entities;
using Eparafia.Application.Enums;

namespace Eparafia.Application.DTOs;

public class IntentionDTO
{
    public DateTime Date { get; set; }
    public string Content { get; set; }
    public IntentionType Type { get; set; }
    public bool AutomaticAllocation { get; set; }
    public bool IsNovena { get; set; }
    
    public static IntentionDTO FromEntity(Intention intention)
    {
        if(intention is null)
            return null;
        return new IntentionDTO
        {
            Date = intention.Date,
            Content = intention.Content,
            Type = intention.Type,
            AutomaticAllocation = intention.AutomaticAllocation,
            IsNovena = intention.IsNovena
        };
    }
}