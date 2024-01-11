﻿using System.Threading;
using System.Threading.Tasks;

namespace Domain.Abstractions;

internal interface IUnitOfWork
{
    // toma todos los cambios en memoria ram 
    // y los guarda en las tablas de la base de datos
    Task<int> SaveChanges(CancellationToken cancellationToken = default);

}

