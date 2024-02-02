using Domain.Commons.Clases;
using MediatR;

namespace Application.Commons.Interfaces;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
