using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Users.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    // !Ojo el método Add no guarda un usuario en la tabla de base de datos
    // solo guarda el usuario en memoria ram
    void Add(User user);
}

