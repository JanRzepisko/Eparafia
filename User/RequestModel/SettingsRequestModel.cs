using eparafia.Helpers;

namespace eparafia.Models;

public class SettingsRequestModel : Request
{
    public SettingsRequestModel(UserSettingsMode mode, int id, object value)
    {
        Mode = mode;
        Id = id;
        Value = value;
    }

    public int Id { get; }
    public UserSettingsMode Mode { get; }
    public object Value { get; }
}