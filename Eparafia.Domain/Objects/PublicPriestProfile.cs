using Eparafia.Application.Enums;
using Eparafia.Domain.Entities;
using Eparafia.Domain.Enums;

namespace Eparafia.Domain.Objects;

public class PublicPriestProfile
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhotoPath { get; set; }
    public string PhotoPathMin { get; set; }
    public FunctionParish FunctionParish { get; set; }


    public static PublicPriestProfile Create(Priest priest)
    {
        return new PublicPriestProfile
        {
            Id = priest.Id,
            Name = priest.Name,
            Surname = priest.Surname,
            PhotoPath = priest.PhotoPath,
            PhotoPathMin = priest.PhotoPathMin,
            FunctionParish = priest.FunctionParish
        };
    }
}