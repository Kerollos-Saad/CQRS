using Domain.Todos;
using Microsoft.EntityFrameworkCore;

namespace Interfaces;

public interface IAppDbContext
{
    DbSet<Todo> Todos {get;}
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}