using Emovere.SharedKernel.Responses;
using MidR.Interfaces;

namespace Emovere.SharedKernel.Abstractions
{
    public interface IPagedQuery<T> : IRequest<PagedResponse<T>>
    { }
}