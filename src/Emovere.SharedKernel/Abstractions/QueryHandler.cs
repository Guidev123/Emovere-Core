using Emovere.SharedKernel.Notifications;
using Emovere.SharedKernel.Responses;
using MidR.Interfaces;

namespace Emovere.SharedKernel.Abstractions
{
    public abstract class QueryHandler<TQuery, TResult>(INotificator notificator) : Handler(notificator), IRequestHandler<TQuery, Response<TResult>>
        where TQuery : IRequest<Response<TResult>>, IQuery<TResult>
        where TResult : class
    {
        public abstract Task<Response<TResult>> ExecuteAsync(TQuery request, CancellationToken cancellationToken);
    }
}