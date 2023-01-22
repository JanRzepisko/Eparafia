using Eparafia.Application.DataAccess;

namespace Eparafia.Application.Services;

public interface IIntentionService
{
    Task<DateTime> CalculateNextIntentionDateAsync(Guid parishId, string content, IUnitOfWork unitOfWork,
        CancellationToken cancellationToken, int startWeek = 0);
}