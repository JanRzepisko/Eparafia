using eparafia.Helpers;

namespace eparafia.Models;

[Serializable]
public class SettingsRequestModel : Request
{
    public SettingsRequestModel(UserSettingsMode mode, int id, object value, string token) : base(token)
    {
        Mode = mode;
        Id = id;
        Value = value; 

    }

    public int Id { get; }
    public UserSettingsMode Mode { get; }
    public object Value { get; }
}