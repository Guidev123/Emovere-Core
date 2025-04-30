using Emovere.SharedKernel.Notifications;
using Emovere.SharedKernel.Responses;
using MidR.Interfaces;

namespace Emovere.SharedKernel.Abstractions
{
    public abstract class PagedQueryHandler<TQuery, TResult>(INotificator notificator) : Handler(notificator), IRequestHandler<TQuery, PagedResponse<TResult>>
        where TQuery : IRequest<PagedResponse<TResult>>, IPagedQuery<TResult>
        where TResult : class
    {
        public abstract Task<PagedResponse<TResult>> ExecuteAsync(TQuery request, CancellationToken cancellationToken);
    }
}