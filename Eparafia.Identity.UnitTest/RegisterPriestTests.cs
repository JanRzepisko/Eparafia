using Eparafia.Identity.Application.Actions.Priest;
using Eparafia.Identity.Application.DataAccess;
using Eparafia.Identity.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using Shared.EventBus;
using Shared.Messages;
using Xunit;

namespace Eparafia.Identity.UnitTest;

public class RegisterPriestTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
    private readonly Mock<IEventBus> _eventBusMock = new Mock<IEventBus>();

    [Fact]
    public async Task Handle_WhenPriestExists_ThrowsException()
    {
        // Arrange
        var existingPriest = new Domain.Entities.Priest();
        _unitOfWorkMock.Setup(uow => uow.Priests.GetByLoginAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingPriest);
        var handler = new RegisterPriest.Handler(_unitOfWorkMock.Object, _eventBusMock.Object);
        var command = new RegisterPriest.Command("Jan", "Kowalski", "jan.kowalski@example.com", "Password123!",
            "Password123!", new Contact("123456789", "jan.kowalski@example.com"));

        // Act
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Priest already exists");
    }

    [Fact]
    public async Task Handle_WhenPriestDoesNotExist_AddsNewPriest()
    {
        // Arrange
        _unitOfWorkMock.Setup(uow => uow.Priests.GetByLoginAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Domain.Entities.Priest)null);
        _unitOfWorkMock.Setup(uow => uow.Priests.AddAsync(It.IsAny<Domain.Entities.Priest>(), It.IsAny<CancellationToken>()));
        _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()));
        var handler = new RegisterPriest.Handler(_unitOfWorkMock.Object, _eventBusMock.Object);
        var command = new RegisterPriest.Command("Jan", "Kowalski", "jan.kowalski@example.com", "Password123!",
            "Password123!", new Contact("123456789", "j.kowalski@example.com"));

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(
            uow => uow.Priests.GetByLoginAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.Priests.AddAsync(It.IsAny<Domain.Entities.Priest>(), It.IsAny<CancellationToken>()),
            Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public void Validator_WhenNameIsNull_ReturnsError()
    {
        var validator = new RegisterPriest.Handler.Validator();
        // Arrange
        var command = new RegisterPriest.Command(null, "Kowalski", "jan.kowalski@example.com", "Password123!",
            "Password123!", new Contact("", ""));

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.True(result.Errors.Count == 2);
    }

    [Fact]
    public void Validator_WhenNameIsEmpty_ReturnsError()
    {
        var validator = new RegisterPriest.Handler.Validator();
        // Arrange
        var command = new RegisterPriest.Command("", "Kowalski", "jan.kowalski@example.com", "Password123!", "Password123!", new Contact("", ""));

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.True(result.Errors.Count == 2); //Because Name must be longer then 3 characters
    }

    [Fact]
    public async Task Handle_GivenValidData_ShouldCreateNewPriestAndPublishEvent()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock
            .Setup(x => x.Priests.GetByLoginAsync(It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync((Domain.Entities.Priest)null);

        var eventBusMock = new Mock<IEventBus>();
        var handler = new RegisterPriest.Handler(unitOfWorkMock.Object, eventBusMock.Object);

        var command = new RegisterPriest.Command(
            "John",
            "Doe",
            "new-priest@test.com",
            "TestPassword123!",
            "TestPassword123!",
            new Contact("123456789", "test-address")
        );

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        unitOfWorkMock.Verify(x => x.Priests.AddAsync(It.IsAny<Domain.Entities.Priest>(), CancellationToken.None), Times.Once);
        unitOfWorkMock.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        eventBusMock.Verify(x => x.PublishAsync(It.IsAny<PriestCreatedBusEvent>(), CancellationToken.None), Times.Once);
    }
}