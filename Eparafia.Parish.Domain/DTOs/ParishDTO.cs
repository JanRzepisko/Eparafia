using Eparafia.Domain.Entities;
using Eparafia.Domain.Objects;
using Eparafia.Domain.ValueObjects;

namespace Eparafia.Domain.DTOs;

public class ParishDTO
{
    public Guid Id { get; set; }
    public string CallName { get; set; }
    public string ShortName { get; set; }
    public Contact Contact { get; set; }
    public Address Address { get; set; }
    public List<PublicPriestProfile> Priests { get; set; }

    public static ParishDTO FromEntity(Parish parish)
    {
        return new ParishDTO
        {
            Id = parish.Id,
            ShortName = parish.ShortName,
            CallName = parish.CallName,
            Contact = parish.Contact,
            Address = parish.Address,
            Priests = parish.Priests.Select(c => new PublicPriestProfile
            {
                Id = c.Id,
                Name = c.Name,
                Photo = c.PhotoPath,
                FunctionParish = c.FunctionParish
            }).ToList()
        };
    }
}