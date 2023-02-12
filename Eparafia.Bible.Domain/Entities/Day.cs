using System.Diagnostics;
using Eparafia.Bible.Domain.Enums;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Bible.Domain.Entities;

public class Day : Entity
{
    public string Name { get; set; }
    public YearType Year { get; set; }
    public ICollection<Reading> Readings { get; set; }
}