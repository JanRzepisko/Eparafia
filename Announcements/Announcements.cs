namespace eparafia.Announcements;

public class Announcements
{
    public Announcements(int id, string title, string content, string date, int parafiaId)
    {
        Id = id;
        Title = title;
        Content = content;
        Date = date;
        ParafiaId = parafiaId;
    }

    public int Id { get; }
    public string Title { get; }
    public string Content { get; }
    public string Date { get; }
    public int ParafiaId { get; }
}