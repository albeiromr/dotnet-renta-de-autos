using Domain.Commons.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Databases;

public sealed class ApplicationDbContext: DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public Task<int> SaveChanges(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
