using Eparafia.Application.DataAccess.Abstract;
using Eparafia.Application.Enums;

namespace Eparafia.Application.Entities;

public class Priest : UserModel
{
    public FunctionParish FunctionParish { get; set; }
    public Contact Contact { get; set; }
}