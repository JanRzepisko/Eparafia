using Eparafia.Application.Services.Jwt;
using Eparafia.Identity.Application.Actions.Priest;
using Eparafia.Identity.Application.DataAccess;
using Eparafia.Identity.Application.Services;
using FluentAssertions;
using Moq;
using Shared.BaseModels.Exceptions;
using Shared.BaseModels.Jwt;
using Xunit;

namespace Eparafia.Identity.UnitTest;

public class LoginPriestTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IJwtAuth> _jwtAuthMock;

    public LoginPriestTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _jwtAuthMock = new Mock<IJwtAuth>();
    }

    [Fact]
    public async Task Handler_WithValidCredentials_ShouldReturnGeneratedToken()
    {
        // Arrange
        var query = new LoginPriest.Query("test@example.com", "Password123");
        var token = new GeneratedToken("access_token");
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(query.Password);
        var priest = new Domain.Entities.Priest
        {
            Email = query.Email,
            PasswordHash = passwordHash,
            IsActive = true
        };
        _unitOfWorkMock.Setup(x => x.Priests.GetByLoginAsync(query.Email, CancellationToken.None))
            .ReturnsAsync(priest);
        _jwtAuthMock.Setup(x => x.GenerateJwt(priest, JwtPolicies.Priest)).ReturnsAsync(token);
        var handler = new LoginPriest.Handler(_unitOfWorkMock.Object, _jwtAuthMock.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().Be(token);
    }

    [Fact]
    public void Handler_WithInvalidCredentials_ShouldThrowBadPasswordException()
    {
        // Arrange
        var query = new LoginPriest.Query("test@example.com", "Password123");
        var passwordHash = BCrypt.Net.BCrypt.HashPassword("AnotherPassword");
        var priest = new Domain.Entities.Priest
        {
            Email = query.Email,
            PasswordHash = passwordHash,
            IsActive = true
        };
        _unitOfWorkMock.Setup(x => x.Priests.GetByLoginAsync(query.Email, CancellationToken.None))
            .ReturnsAsync(priest);
        var handler = new LoginPriest.Handler(_unitOfWorkMock.Object, _jwtAuthMock.Object);

        // Act
        Func<Task> action = async () => await handler.Handle(query, CancellationToken.None);

        // Assert
        action.Should().ThrowAsync<BadPassword>().WithMessage("Bad password");
    }

    [Fact]
    public void Handler_WithNonExistingUser_ShouldThrowEntityNotFoundException()
    {
        // Arrange
        var query = new LoginPriest.Query("test@example.com", "Password123");
        _unitOfWorkMock.Setup(x => x.Priests.GetByLoginAsync(query.Email, CancellationToken.None))
            .ReturnsAsync((Domain.Entities.Priest)null);
        var handler = new LoginPriest.Handler(_unitOfWorkMock.Object, _jwtAuthMock.Object);

        // Act
        Func<Task> action = async () => await handler.Handle(query, CancellationToken.None);

        // Assert
        action.Should().ThrowAsync<EntityNotFoundException>().WithMessage("User not found");
    }

}