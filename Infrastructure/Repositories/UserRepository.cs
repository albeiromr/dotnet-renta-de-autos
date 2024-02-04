using Domain.Users;
using Domain.Users.Interfaces;
using Infrastructure.Databases;

namespace Infrastructure.Repositories;

internal sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext? context) : base(context)
    {
    }
}
