
namespace Eparafia.Administration.Domain.ValueObjects;

public class BaptismClient
{
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateOnly BaptismDate { get; set; }
}