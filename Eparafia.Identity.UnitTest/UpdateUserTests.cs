using Eparafia.Application.Enums;
using Eparafia.Application.Services.FileManager;
using Eparafia.Identity.Application.Actions.Priest;
using Eparafia.Identity.Application.DataAccess;
using Eparafia.Identity.Domain.Entities;
using Eparafia.Identity.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using Shared.EventBus;
using Shared.Messages;
using Shared.Service.Interfaces;
using Xunit;

namespace Eparafia.Identity.UnitTest;

public class UpdatePriestTests
{
    [Fact]
    public async Task Handle_WithValidCommand_UpdatesPriest()
    {
        Guid priestId = Guid.NewGuid();

        // Arrange
        var userProviderMock = new Mock<IUserProvider>();
        userProviderMock.Setup(x => x.Id).Returns(priestId);

        var priest = new Priest
        {
            Id = Guid.NewGuid(),
            Name = "John",
            Surname = "Doe",
            Email = "john.doe@test.com",
            Contact = new Contact( "123123123","john.doe@test.com"),
            PhotoPath = "/path/to/photo",
            PhotoPathMin = "/path/to/photo-min"
        };

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(x => x.Priests.GetByIdAsync(priestId, default)).ReturnsAsync(priest);

        var fileManagerMock = new Mock<IFileManager>();
        fileManagerMock.Setup(x =>
                x.SaveImageAsync(It.IsAny<string>(), It.IsAny<ImageType>(), It.IsAny<Guid>(), default)).ReturnsAsync(new Tuple<string, string>("", ""));
        var eventBusMock = new Mock<IEventBus>();

        var command = new UpdatePriest.Command(
            "Jane", "Doe", "jane.doe@test.com", "base64-encoded-image", true, new Contact("123123123", "jane.doe@test.com"));

        var handler = new UpdatePriest.Handler(unitOfWorkMock.Object, userProviderMock.Object,
            fileManagerMock.Object, eventBusMock.Object);

        // Act
        await handler.Handle(command, default);

        // Assert
        unitOfWorkMock.Verify(x => x.Priests.GetByIdAsync(priestId, default), Times.Once);
        unitOfWorkMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        eventBusMock.Verify(x => x.PublishAsync(It.IsAny<PriestUpdatedBusEvent>(), default), Times.Once);

        priest.Name.Should().Be("Jane");
        priest.Surname.Should().Be("Doe");
        priest.Email.Should().Be("jane.doe@test.com");
        priest.PhotoPath.Should().BeEmpty();
        priest.PhotoPathMin.Should().BeEmpty();
        priest.Contact.Should().BeEquivalentTo(new Contact("123123123", "jane.doe@test.com"));
    }

    [Fact]
    public async Task Handle_WithInvalidCommand_ThrowsValidationException()
    {
        // Arrange
        Guid priestId = Guid.NewGuid();
        
        var userProviderMock = new Mock<IUserProvider>();
        userProviderMock.Setup(x => x.Id).Returns(priestId);

        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var fileManagerMock = new Mock<IFileManager>();

        var eventBusMock = new Mock<IEventBus>();

        var command = new  UpdatePriest.Command(
            "Jo", "", "not-an-email", "base64-encoded-image", true, null);


        var handler = new UpdatePriest.Handler(unitOfWorkMock.Object, userProviderMock.Object,
            fileManagerMock.Object, eventBusMock.Object);

        handler.Handle(command, default);
    }
}