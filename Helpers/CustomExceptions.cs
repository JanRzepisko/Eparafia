using Microsoft.Extensions.Primitives;

namespace eparafia.Helpers;

[Serializable]
public class UserIsNotExist : Exception
{ 
    public UserIsNotExist() {}
}