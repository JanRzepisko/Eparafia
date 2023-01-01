using Eparafia.Application.DTOs.Resluts;
using Eparafia.Application.Entities;

namespace Eparafia.Application.DTOs;

public class ParishDTO
{
    public Guid Id { get; set; }
    public string CallName { get; set; }
    public Contact Contact { get; set; }
    public Address Address { get; set; }
    public List<PublicPriestProfile> Priests { get; set; }
    
    public static ParishDTO FromEntity(Parish parish)
    {
        return new ParishDTO
        {
            Id = parish.Id,
            CallName = parish.CallName,
            Contact = parish.Contact,
            Address = parish.Address,
            Priests = parish.Priests.Select(c => new PublicPriestProfile()
            {
                Id = c.Id,
                Name = c.Name,
                HasAvatar = c.HasAvatar,
                Surname = c.Surname,
                FunctionParish = c.FunctionParish,
            }).ToList()
        };
    }
}
