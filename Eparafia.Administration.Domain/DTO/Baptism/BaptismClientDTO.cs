namespace Eparafia.Administration.Domain.DTO.Baptism;

public class BaptismClientDTO
{
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateOnly BaptismDate { get; set; }
}