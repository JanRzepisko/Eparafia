using Eparafia.Bible.Domain.Enums;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Bible.Domain.Entities;

public class Reading : Entity
{
    public ReadingType Type { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid DayId { get; set; }
    public Day Day { get; set; }
}