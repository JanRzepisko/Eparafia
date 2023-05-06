using Eparafia.Administration.Domain.DefaultModel;

namespace Eparafia.Administration.Domain.Entities.WeddingEntities;

public class WeddingRegister : RegisterModel
{
    public Men Men { get; set; }
    public Guid MenId { get; set; }
    public Women Women { get;set; } 
    public Guid WomenId { get; set; }
    public ICollection<Witness> Witnesses { get; set; }

}