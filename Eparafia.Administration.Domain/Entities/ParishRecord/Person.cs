using Shared.BaseModels.BaseEntities;

namespace Eparafia.Administration.Domain.Entities.ParishRecord;

public class Person : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Job { get; set; }
    
    public HomeRecord HomeRecord { get; set; }
    public Guid HomeRecordId { get; set; }
}