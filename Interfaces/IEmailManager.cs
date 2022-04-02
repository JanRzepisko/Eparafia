namespace eparafia;

public interface IEmailManager
{
    public Task SendEmail(string email);
}