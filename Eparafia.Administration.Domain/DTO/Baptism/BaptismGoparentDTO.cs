using Eparafia.Administration.Domain.Enums;
using Eparafia.Domain.ValueObjects;

namespace Eparafia.Administration.Domain.DTO.Baptism;

public class BaptismGoparentDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Job { get; set; }
    public string CityOfBirth { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Confession Confession { get; set; }
    public Address Address { get; set; }
}