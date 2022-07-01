namespace eparafia.Intention;

public class Intention
{
    public IntentionType Type { get; }
    public string Value { get; }

    public Intention(IntentionType type, string value)
    {
        Type = type;
        Value = value;
    }

    public Intention()
    {
        Type = 0;
        Value = "Brak";
    }
}