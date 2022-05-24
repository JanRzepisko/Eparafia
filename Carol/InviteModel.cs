namespace eparafia.Carol;

public class InviteModel
{
    public InviteModel(string address, int index, int id)
    {
        Address = address;
        Index = index;
        Id = id;
    }    
    public InviteModel(string address, int index, int id, int priest, DateTime date)
    {
        Address = address;
        Index = index;
        Id = id;
        Priest = priest;
        Date = date;
    }

    public int Id { get; }
    public string Address { get; }
    public int Index { get; set; }
    public int Priest { get; set; }
    public DateTime Date {get; set; }
}