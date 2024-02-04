using Domain.Commons.Clases;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal abstract class Repository<T>
    where T : Entity
{
    protected readonly ApplicationDbContext? _context;

    protected Repository(ApplicationDbContext? context)
    {
        _context = context;
    }

    // método génerico que permite devolver un método por el Id
    public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var dbSet = _context!.Set<T>();
        var data = await dbSet.FirstOrDefaultAsync(entity => entity.Id == id);
        return data!;
    }

    // Agrega un nuevo registro a la base de datos
    public void Add(T entity)
    {
        _context!.Add(entity);
    }
}   
