using eparafia.Models;

namespace eparafia.Carol;

public class UserCarol
{
    public DateTime CarolDate;
    public Priest.Priest Priest;
    public int CarolId;

    public UserCarol(DateTime carolDate, Priest.Priest priest, int carolId)
    {
        CarolDate = carolDate;
        Priest = priest;
        CarolId = carolId;
    }
}