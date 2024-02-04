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

    // aplicando las configuraciones de las tablas
    // esto significa que cuando las entidades dean cargadas este método
    // escaneará todo el assembly buscando todas las configuraciones de las entidades
    // para aplicar dichas configuraciones a las entidades
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
