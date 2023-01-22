using Eparafia.Application.DataAccess.Abstract;

namespace Eparafia.Application.Entities;

public class Payments : Entity
{
    public decimal Amount { get; set; }
}