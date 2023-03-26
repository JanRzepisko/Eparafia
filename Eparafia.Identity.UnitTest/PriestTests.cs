using Eparafia.Application.Services.Jwt;
using Eparafia.Identity.Application.Actions.Priest;
using Eparafia.Identity.Application.DataAccess;
using Eparafia.Identity.Application.Services;
using Eparafia.Identity.Domain.ValueObjects;
using MediatR;
using Moq;
using Shared.EventBus;
using Xunit;

namespace Eparafia.Identity.UnitTest;

public class PriestTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IEventBus> _eventBus;
    private readonly Mock<IJwtAuth> _jwtAuth;

    public PriestTests()
    {
        _unitOfWork = new ();
        _eventBus = new();
        _jwtAuth = new();
    }

    [Fact]
    public async Task RegisterTest()
    {
        _unitOfWork.Setup(c => c.Priests.GetByLoginAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync((Domain.Entities.Priest) null);
        
        var command = new RegisterPriest.Command("Janek", "Test", "test@test.com", "Trudnehaslo123!","Trudnehaslo123!", new Contact("123456789", "test@test.com"));
        var handler = new RegisterPriest.Handler(_unitOfWork.Object, _eventBus.Object);
        
        var result = await handler.Handle(command, default);
        
        Assert.Equal(Unit.Value, result);
    }
    
    [Fact]
    public async Task LoginTest()
    {   
        _unitOfWork.Setup(c => c.Priests.GetByLoginAsync("test@test.com", It.IsAny<CancellationToken>()));//.ReturnsAsync((Domain.Entities.Priest) null);
        var command = new LoginPriest.Query("rzejan@gmail.com", "Trudnehaslo123!");
        var handler = new LoginPriest.Handler(_unitOfWork.Object, _jwtAuth.Object);
        
        var result = await handler.Handle(command, default);

        Assert.IsType(typeof(GeneratedToken), result);
        Assert.NotEmpty(result.Jwt);
    }
}