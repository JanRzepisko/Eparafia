using Eparafia.Domain.Entities;

namespace Eparafia.Domain.DTOs;

public class PriestDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ParishDTO Parish { get; set; }
    
    public static PriestDTO FromEntity(Priest priest)
    {
        return new PriestDTO
        {
            Id = priest.Id,
            Name = priest.Name,
            Parish = ParishDTO.FromEntity(priest.Parish)
        };
    }
}