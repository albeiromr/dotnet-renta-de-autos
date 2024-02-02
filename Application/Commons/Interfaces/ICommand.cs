﻿using Domain.Commons.Clases;
using MediatR;

namespace Application.Commons.Interfaces;

public interface ICommand : IRequest<Result>, IBaseCommand
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
}

public interface IBaseCommand
{
}