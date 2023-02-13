using Eparafia.Application.Services.FileManager;
using Eparafia.Domain.Entities;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Domain.Objects;

public class UserModel : Entity
{
    public string Name { get; set; }
    public Guid? ParishId { get; set; }
    public Parish Parish { get; set; }
    public PhotoPath PhotoPath { get; set; }

}