using Eparafia.Application.Entities;
using Eparafia.Application.Enums;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;

namespace Eparafia.Application.DTOs.Rezluts;

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