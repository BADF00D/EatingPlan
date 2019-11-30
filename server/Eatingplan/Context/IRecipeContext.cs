using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Eatingplan.Model;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Eatingplan
{
    public interface IRecipeContext
    {
        bool Any();
        EntityEntry<Recipe> Add(Recipe item);
        ValueTask<EntityEntry<Recipe>> AddAsync(Recipe item);
        Task<List<Recipe>> ToListAsync();
        ValueTask<Recipe> FindAsync(long id);
        EntityEntry<Recipe> Update(Recipe item);
        EntityEntry<Recipe> Remove(Recipe item);
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}