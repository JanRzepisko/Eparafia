namespace eparafia.Helpers;

[Serializable]
public class Request
{
    //public string? Token;
    public string? Token { get; }

    protected Request()
    {
    }    
    public Request(string? token)
    {
        Token = token;
    }

    public virtual Task Validate()
    {
        return Task.CompletedTask;
    }
}