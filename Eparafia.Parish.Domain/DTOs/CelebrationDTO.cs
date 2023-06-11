using Eparafia.Domain.Enums;

namespace Eparafia.Domain.Objects;

public class CelebrationDTO
{
    public string Title { get; set; }
    public Colour Colour { get; set; }
    public Rank Rank { get; set; }
    public double RankNum { get; set; }
}