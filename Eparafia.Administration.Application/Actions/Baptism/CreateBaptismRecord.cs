using Eparafia.Administration.Application.DataAccess;
using Eparafia.Administration.Domain.DTO.Baptism;
using FluentValidation;
using MediatR;
using Shared.BaseModels.Exceptions;
using Shared.Service.Interfaces;

namespace Eparafia.Administration.Application.Actions.Baptism;

public static class CreateBaptismRecord
{
    public sealed record Command(BaptismRegisterDTO Record) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProvider _userProvider;

        public Handler(IUnitOfWork unitOfWork, IUserProvider userProvider)
        {
            _userProvider = userProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var priest = await _unitOfWork.Priests.GetByIdAsync(_userProvider.Id,cancellationToken);
            if (priest is null)
            {
                throw new EntityNotFoundException("Priest not found");
            }
            await _unitOfWork.BaptismRegister.AddAsync(request.Record.FromDtoToEntity((Guid)priest.ParishId), cancellationToken);
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Record.ActId).NotEmpty();
                RuleFor(c => c.Record.Client.Surname).NotEmpty().MinimumLength(3);
                RuleFor(c => c.Record.Client.FirstName).NotEmpty().MinimumLength(3);
                RuleFor(c => c.Record.Client.BirthDate).NotEmpty().GreaterThan(DateOnly.Parse(DateTime.Today.ToString()));
                RuleFor(c => c.Record.Client.BaptismDate).NotEmpty().GreaterThan(DateOnly.Parse(DateTime.Today.ToString()));
                RuleFor(c => c.Record.Parents.ParentRelation).NotEmpty();
                RuleFor(c => c.Record.Parents.Father.FirstName).NotEmpty().MinimumLength(3);
                RuleFor(c => c.Record.Parents.Father.LastName).NotEmpty().MinimumLength(3);
                RuleFor(c => c.Record.Parents.Father.Job).NotEmpty().MinimumLength(3);
                RuleFor(c => c.Record.Parents.Father.CityOfBirth).NotEmpty().MinimumLength(3);
                RuleFor(c => c.Record.Parents.Father.DateOfBirth).NotEmpty().GreaterThan(DateOnly.Parse(DateTime.Today.ToString()));
                RuleFor(c => c.Record.Parents.Father.Confession).NotEmpty();
                RuleFor(c => c.Record.Parents.Father.Address.City).MinimumLength(3);
                RuleFor(c => c.Record.Parents.Father.Address.Street).MinimumLength(3); 
                RuleFor(c => c.Record.Parents.Mother.FirstName).NotEmpty().MinimumLength(3);
                RuleFor(c => c.Record.Parents.Mother.LastName).NotEmpty().MinimumLength(3);
                RuleFor(c => c.Record.Parents.Mother.Job).NotEmpty().MinimumLength(3);
                RuleFor(c => c.Record.Parents.Mother.CityOfBirth).NotEmpty().MinimumLength(3);
                RuleFor(c => c.Record.Parents.Mother.DateOfBirth).NotEmpty().GreaterThan(DateOnly.Parse(DateTime.Today.ToString()));
                RuleFor(c => c.Record.Parents.Mother.Confession).NotEmpty();
                RuleFor(c => c.Record.Parents.Mother.Address.City).MinimumLength(3);
                RuleFor(c => c.Record.Parents.Mother.Address.Street).MinimumLength(3);
                RuleFor(c => c.Record.Godmother.FirstName).NotEmpty().MinimumLength(3);
                RuleFor(c => c.Record.Godmother.LastName).NotEmpty().MinimumLength(3);
                RuleFor(c => c.Record.Godmother.Job).NotEmpty().MinimumLength(3);
                RuleFor(c => c.Record.Godmother.CityOfBirth).NotEmpty().MinimumLength(3);
                RuleFor(c => c.Record.Godmother.DateOfBirth).NotEmpty().GreaterThan(DateOnly.Parse(DateTime.Today.ToString()));
                RuleFor(c => c.Record.Godmother.Confession).NotEmpty();
                RuleFor(c => c.Record.Godmother.Address.City).MinimumLength(3);
                RuleFor(c => c.Record.Godmother.Address.Street).MinimumLength(3);
                RuleFor(c => c.Record.Godfather.FirstName).NotEmpty().MinimumLength(3);
                RuleFor(c => c.Record.Godfather.LastName).NotEmpty().MinimumLength(3);
                RuleFor(c => c.Record.Godfather.Job).NotEmpty().MinimumLength(3);
                RuleFor(c => c.Record.Godfather.CityOfBirth).NotEmpty().MinimumLength(3);
                RuleFor(c => c.Record.Godfather.DateOfBirth).NotEmpty().GreaterThan(DateOnly.Parse(DateTime.Today.ToString()));
                RuleFor(c => c.Record.Godfather.Confession).NotEmpty();
                RuleFor(c => c.Record.Godfather.Address.City).MinimumLength(3);
                RuleFor(c => c.Record.Godfather.Address.Street).MinimumLength(3);
            }
        }
    }
}