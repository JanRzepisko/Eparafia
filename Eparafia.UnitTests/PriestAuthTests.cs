using Eparafia.Application.DataAccess;
using Eparafia.Application.Entities;
using MediatR;
using Moq;

namespace Eparafia.UnitTests;

using Features = Application.Actions.PriestAuth;

public class PriestAuthTests
{
    private readonly IUnitOfWork _unitOfWork;
    
    private string Name = "Janek";
    private string Surname = "Kowalski";
    private string Email = "mail@mail.com";
    private string Password = "Trudnehaslo123!";
    private string PhoneNumber = "123456789";

    public PriestAuthTests(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [Fact]
    public async void Register_Priest()
    {
        Console.WriteLine("xd");
        var command = new Features.Command.RegisterPriest.Command(Name, Surname, Email, Password, Password, new Contact(PhoneNumber, Email));
        var handler = new Features.Command.RegisterPriest.Handler(_unitOfWork);
        
        Unit result = await handler.Handle(command, default);
        Assert.NotEmpty(new[] { result });
    }
}