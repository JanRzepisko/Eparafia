using Eparafia.Application.DataAccess;
using Eparafia.Application.Enums;
using Eparafia.Application.Services.FileManager;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.BaseModels.Exceptions;
using Shared.Service.Interfaces;

namespace Eparafia.Application.Actions.User.Command;

public static class UpdateUser
{
    public sealed record Command(Guid UserId, string Name, string PhotoPath, string PhotoPathMin) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Priests.GetByIdAsync(request.UserId, cancellationToken);

            user.Name = request.Name;
            user.PhotoPath = new PhotoPath(request.PhotoPath, request.PhotoPathMin);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                
            }
        }
    }
}