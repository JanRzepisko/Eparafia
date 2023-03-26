using Eparafia.Application.DataAccess;
using Eparafia.Application.Services.FileManager;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.EventConsumerActions.User.Command;

public static class CreateUser
{
    public sealed record Command(Guid Id, string Name, string PhotoPath, string PhotoPathMin) : IRequest<Unit>;

    public sealed class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var newUser = new Domain.Entities.User
            {
                Id = request.Id,
                Name = request.Name,
                PhotoPath = new PhotoPath(request.PhotoPath, request.PhotoPathMin),
                ParishId = null
            };

            await _unitOfWork.Users.AddAsync(newUser, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

    public sealed class Validator : AbstractValidator<Command>
    {
    }
}