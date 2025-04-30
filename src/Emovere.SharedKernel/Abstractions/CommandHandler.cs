using Emovere.SharedKernel.Notifications;
using Emovere.SharedKernel.Responses;
using MidR.Interfaces;

namespace Emovere.SharedKernel.Abstractions
{
    public abstract class CommandHandler<TCommand, TResult>(INotificator notificator)
                        : Handler(notificator), IRequestHandler<TCommand, Response<TResult>>
                          where TCommand : Command<TResult>, IRequest<Response<TResult>>
                          where TResult : class

    {
        public abstract Task<Response<TResult>> ExecuteAsync(TCommand request, CancellationToken cancellationToken);
    }
}