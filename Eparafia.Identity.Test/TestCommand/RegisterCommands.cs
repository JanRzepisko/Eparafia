using Eparafia.Identity.Application.Actions.Priest;
using Eparafia.Identity.Domain.ValueObjects;

namespace Eparafia.Identity.Test.Integration.TestCommand;

public class RegisterCommands
{ 
    public static RegisterPriest.Command RegisterCommand = new("Test,", "Test","test@test.test", "Trudnehaslo123!", "Trudnehaslo123!", new Contact("123123123", "test@test.test"));
    public static RegisterPriest.Command RegisterCommand = new("Test,", "Test","test@test.test", "Trudnehaslo123!", "Trudnehaslo123!", new Contact("123123123", "test@test.test"));

}