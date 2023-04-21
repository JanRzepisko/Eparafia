using Eparafia.Administration.Domain.DefaultModel;
using Eparafia.Administration.Domain.ValueObjects;

namespace Eparafia.Administration.Domain.Entities;

public class DeadRegister : RegisterModel
{
    public Person Client { get; set; }
    
}