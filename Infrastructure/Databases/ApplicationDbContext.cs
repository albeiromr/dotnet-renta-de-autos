using Application.Commons.Exeptions;
using Domain.Commons.Clases;
using Domain.Commons.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Databases;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;
    public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
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

    // sobreescribiendo este método que pertenece a la interfaz
    // IUnitOfWork
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            await DispatchAllDomainEventsAsync();
            return result;
        } catch (DbUpdateConcurrencyException ex)
        {
            // Cuando estamos escribiendo datos en una base de datos y otro usuario también está 
            // tratando de escribir datos, esta operación generará errores por temas de concurrencia
            // y la db arrojará una execepción de tipo DbUpdateConcurrencyException
            throw new ConcurrencyException("A concurrency exception was triggered", ex);
        }
    }

    //El siguiente método se explica en el video número 46 del curso de udemy (unit of work)
    private async Task DispatchAllDomainEventsAsync()
    {
        // generando una consulta para obtener todos los Domainevents que están esperando por 
        // der dispatcheados en cada entidad
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var events = entity.GetDomainEvents();
                entity.ClearDomainEvents();
                return events;
            }).ToList();

        // Haciendo el dispatch de los domain events obtenidos
        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }

    }
}
