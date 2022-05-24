using eparafia.Helpers;

namespace eparafia.Carol.RequestModel;

public class GenerateCarolRequestModel : Request
{
    public GenerateCarolRequestModel(int id, string token, string startCarol, List<PriestCarolCalendar> priestCalendar) : base(token)
    {
        Id = id;
        StartCarol = startCarol;
        PriestCalendar = priestCalendar;
    }

    public int Id { get; }
    public List<PriestCarolCalendar> PriestCalendar { get; }
    public string StartCarol { get; }
    
}

public class PriestCarolCalendar
{
    public PriestCarolCalendar(int priestId, List<BreakdownOfCarolsHours> calendar)
    {
        PriestId = priestId;
        Calendar = calendar;
    }

    public int PriestId { get; }
    public List<BreakdownOfCarolsHours> Calendar { get; }
}


public class BreakdownOfCarolsHours
{
    public BreakdownOfCarolsHours(string endHour, string startHour, int dayIndex)
    {
        EndHour = endHour;
        StartHour = startHour;
        DayIndex = dayIndex;
    }

    public int DayIndex { get; }
    public string StartHour { get; }
    public string EndHour { get; }

}