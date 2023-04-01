using Eparafia.Identity.Application.Actions.Priest;
using Eparafia.Identity.Application.DataAccess;
using FluentValidation.TestHelper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Moq;
using Shared.BaseModels.Exceptions;
using Shared.EventBus;
using Shared.Messages;
using Xunit;

namespace Eparafia.Identity.UnitTest;

public class RemovePriestTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
    private readonly Mock<IConfiguration> _configurationMock = new Mock<IConfiguration>();
    private readonly Mock<IEventBus> _eventBusMock = new Mock<IEventBus>();

    [Fact]
    public async Task Handle_WhenPriestExists_ShouldRemovePriestAndPublishEvent()
    {
        // Arrange
        var priestId = Guid.NewGuid();
        var command = new RemovePriest.Command(priestId);
        var handler = new RemovePriest.Handler(_unitOfWorkMock.Object, _configurationMock.Object, _eventBusMock.Object);
        _unitOfWorkMock.Setup(uow => uow.Priests.ExistsAsync(priestId, CancellationToken.None)).ReturnsAsync(true);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(uow => uow.Priests.ExistsAsync(priestId, CancellationToken.None), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.Priests.RemoveById(priestId), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(CancellationToken.None), Times.Once);
        _eventBusMock.Verify(eb => eb.PublishAsync(It.IsAny<PriestRemovedBusEvent>(), CancellationToken.None), Times.Once);
        Assert.Equal(Unit.Value, result);
    }

    [Fact]
    public async Task Handle_WhenPriestDoesNotExist_ShouldThrowEntityNotFoundException()
    {
        // Arrange
        var priestId = Guid.NewGuid();
        var command = new RemovePriest.Command(priestId);
        var handler = new RemovePriest.Handler(_unitOfWorkMock.Object, _configurationMock.Object, _eventBusMock.Object);
        _unitOfWorkMock.Setup(uow => uow.Priests.ExistsAsync(priestId, CancellationToken.None)).ReturnsAsync(false);

        // Act + Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => handler.Handle(command, CancellationToken.None));
        _unitOfWorkMock.Verify(uow => uow.Priests.ExistsAsync(priestId, CancellationToken.None), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.Priests.RemoveById(priestId), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(CancellationToken.None), Times.Never);
        _eventBusMock.Verify(eb => eb.PublishAsync(It.IsAny<PriestRemovedBusEvent>(), CancellationToken.None), Times.Never);
    }

    [Fact]
    public void Validator_WhenPriestIdIsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var validator = new RemovePriest.Handler.Validator();
        var command = new RemovePriest.Command(Guid.Empty);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.PriestId);
    }

    [Fact]
    public void Validator_WhenPriestIdIsNotEmpty_ShouldNotHaveValidationError()
    {
        // Arrange
        var validator = new RemovePriest.Handler.Validator();
        var command = new RemovePriest.Command(Guid.NewGuid());

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.PriestId);
    }
}
