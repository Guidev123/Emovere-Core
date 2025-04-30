using Emovere.SharedKernel.Responses;
using MidR.Interfaces;

namespace Emovere.SharedKernel.Abstractions
{
    public interface IQuery<T> : IRequest<Response<T>>
    { }
}