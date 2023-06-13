using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using Eparafia.Domain.Enums;

namespace Eparafia.Domain.Objects.ResponseModel;

public class LiturgicalCalendarDTO
{
    public DateTime Date { get; set; }
    public Season Season { get; set; }
    public int SeasonWeek { get; set; }
    public DayOfWeek Weekday => Date.DayOfWeek;
    public List<CelebrationDTO> Celebrations { get; set; }

    public static LiturgicalCalendarDTO FromModel(LiturgicalCalendarResponseModel model)
    {
        return new LiturgicalCalendarDTO()
        {
            Celebrations = model.Celebrations.Select(c => new CelebrationDTO()
            {
                Colour = GetColour(c.Colour),
                Rank = GetRank(c.Rank),
                RankNum = c.Rank_Num,
                Title = c.Title
            }).ToList(),
            Date = model.Date,
            Season = GetSeason(model.Season),
            SeasonWeek = model.Season_Week,
        };
    }

    private static Season GetSeason(string season)
    {
        return season switch
        {
            "advent" => Season.Advent,
            "christmas" => Season.Christmas,
            "lent" => Season.Lent,
            "easter" => Season.Easter,
            "ordinary" => Season.Ordinary,
            _ => throw new ArgumentOutOfRangeException(nameof(season), season, null)
        };
    }

    private static Rank GetRank(string rank)
    {
        return rank switch
        {
            "ferial" => Rank.Ferial,
            "optional memorial" => Rank.OptionalMemorial,
            "Easter triduum" => Rank.EasterTriduum,
            "solemnity" => Rank.Solemnity,
            "memorial" => Rank.Memorial,
            "feast" => Rank.Feast,
            "Sunday" => Rank.Sunday,
            "commemoration" => Rank.Commemoration,
            "primary liturgical days" => Rank.PrimaryLiturgicalDays,
            
            _ => throw new ArgumentOutOfRangeException(nameof(rank), rank, null)
        };
    }

    private static Colour GetColour(string colour)
    {
        return colour switch
        {
            "white" => Colour.White,
            "red" => Colour.Red,
            "green" => Colour.Green,
            "violet" => Colour.Violet,
            "black" => Colour.Black,
            "rose" => Colour.Rose,
            _ => throw new ArgumentOutOfRangeException(nameof(colour), colour, null)
        };
    }
}
