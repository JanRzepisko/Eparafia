namespace eparafia;

public interface ISqlManager
{
    public Task<List<Dictionary<string, dynamic>>> Reader(string query);
    public Task Execute(string query);

    public Task<bool> IsValueExist(string query);
}