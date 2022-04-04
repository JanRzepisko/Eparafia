namespace eparafia.Parafia.RequestModel;

public class GetParafiaRequestModel
{
    public GetParafiaRequestModel(int id)
    {
        Id = id;
    }

    public int Id { get; }
}