namespace eparafia.Helpers;

public class Request
{
    private readonly ISqlManager _sqlManager;
    
    public Request(ISqlManager sqlManager)
    {
        _sqlManager = sqlManager;

    }

    protected Request()
    {
        
    }

    public virtual Task Validate()
    {
        return Task.CompletedTask;
    }
}