using eparafia.Helpers;

namespace eparafia.Carol.RequestModel;

public class GetCarol : Request
{
    public GetCarol(int id, string token): base(token)
    {
        Id = id;
    }
    
    public GetCarol(int id, string date, string token): base(token)
    {
        Id = id;
        Date = date;
    }
    
    public int Id { get; }
    public string Date { get; }
}