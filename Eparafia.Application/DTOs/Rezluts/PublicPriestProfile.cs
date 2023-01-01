using Eparafia.Application.Enums;

namespace Eparafia.Application.DTOs.Resluts;

public class PublicPriestProfile
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhotoPath { get; set; }
    public string PhotoPathMin { get; set; }
    public FunctionParish FunctionParish { get; set; }
}