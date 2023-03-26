using Eparafia.Application.Services.FileManager;
using Eparafia.Domain.Entities;
using Eparafia.Domain.Enums;

namespace Eparafia.Domain.Objects;

public class PublicPriestProfile
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public PhotoPath Photo { get; set; }
    public FunctionParish FunctionParish { get; set; }


    public static PublicPriestProfile Create(Priest priest)
    {
        return new PublicPriestProfile
        {
            Id = priest.Id,
            Name = priest.Name,
            Photo = priest.PhotoPath,
            FunctionParish = priest.FunctionParish
        };
    }
}